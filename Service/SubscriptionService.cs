using Entity.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository;
using Contract.IService;
using Contract.IRepository;
using Entity.Dto;
using Entity.Model;
using Exception;
using NLog;

namespace Service;
public class SubscriptionService : ISubscriptionService
{

    private readonly ISubscriptionRepository subscriptionRepository;
    private readonly IConfiguration _config;
    private readonly ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IMapper _mapper;
        
    public SubscriptionService(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _config = config;
        _context = context;
        subscriptionRepository = new SubscriptionRepository(_config, _context,_mapper);
    }

    //To delete subscription by id
    public ResponseDto DeleteSubscriptionById(Guid? subscriptionId, Guid userId)
    {
        Subscription? subscription = subscriptionRepository.GetSubscriptionDetailsById(subscriptionId);
        if(subscription == null)
        {
            throw new BaseCustomException(404, "Subscription id not found", "Please give the valid subscription id");
        }
        else if(subscription.Key.ToUpper() == "FREE")
        {
            throw new BaseCustomException(400, "Free plan cannot be deleted", "Please give the valid subscription id");
        }
        List<Movie> movies = subscriptionRepository.GetMovieDetailsBySubscriptionId(subscriptionId);
        Guid? newSubscriptionId = subscriptionRepository.GetSubscriptionIdByKey("FREE");
        List<Movie> updatedMovies = movies!.Select(m => { m.SubscriptionId = (Guid)newSubscriptionId!; return m; }).ToList();
        
        subscription.IsActive = false;
        subscription.UpdatedBy = userId;
        subscription.UpdatedOn = DateTime.UtcNow;

        subscriptionRepository.SaveMovie(updatedMovies);
        subscriptionRepository.SaveSubscription(subscription);
        ResponseDto responseDto = new ResponseDto()
        {
            StatusCode = 200,
            Message = "Subscription plan deleted successfully",
            Description = "The selected subscription is removed from the database"
        };
        return responseDto;
    }

    public List<SubscriptionDto> GetSubscriptionDetails(string role, Guid userId, int rowSize, int startIndex, string sortOrder)
    {
        List<SubscriptionDto> subscriptionDto = new List<SubscriptionDto>();
        if(role == "Admin")
        {
            List<Subscription> subscription = subscriptionRepository.GetAllSubscriptionDetails(rowSize, startIndex, sortOrder);
            foreach(Subscription oneSubscription in subscription)
            {
                SubscriptionDto dto = _mapper.Map<SubscriptionDto>(oneSubscription);
                subscriptionDto.Add(dto);
            }
        }
        else
        {
            Subscription subscription = subscriptionRepository.GetSubscriptionDetailsById(subscriptionRepository.GetSubscriptionIdByUserId(userId))!;
            SubscriptionDto  dto = _mapper.Map<SubscriptionDto>(subscription);
            subscriptionDto.Add(dto);
        }
        return subscriptionDto;
    }

    //Add new subscription plan to database by admin user
    public ResponseIdDto AddNewSubscription(SubscriptionDto subscriptionDto,Guid userId)
    {
        Guid subscriptionId = Guid.NewGuid();
        subscriptionDto.Id = subscriptionId;
        subscriptionDto.UserId = userId;
        subscriptionDto.CreatedBy = userId;
        subscriptionDto.CreatedOn = DateTime.UtcNow;
        subscriptionDto.Key = subscriptionDto.Key.ToUpper();
        if (subscriptionRepository.GetSubscriptionIdByKey(subscriptionDto.Key) != null)
        {
            throw new BaseCustomException(409, "Subscription plan already exist", "Subscription plan already taken. Please choose a different subscription plan");
        }
        Subscription subscription = _mapper.Map<Subscription>(subscriptionDto);
        subscriptionRepository.CreateSubscription(subscription);
        ResponseIdDto responseIdDto = new ResponseIdDto()
        {
            Id = subscriptionId,
            Message = "New subscription plan created successfully",
        };
        return responseIdDto;
    }

    //Update the subscription details by subscriptionId and subscriptionDto
    public ResponseDto UpdateSubscriptionById(Guid? subscriptionId, SubscriptionDto subscriptionDto, Guid userId)
    {
        Subscription? subscription = subscriptionRepository.GetSubscriptionDetailsById(subscriptionId);
        if(subscription == null)
        {
            throw new BaseCustomException(404, "Subscription id not found", "Please give the valid subscription id");
        }
        else if(subscription.Key.ToUpper() == "FREE")
        {
            throw new BaseCustomException(400, "Free plan cannot be updated", "Please give the valid subscription id");
        }
        if (subscriptionRepository.GetSubscriptionIdByKey(subscriptionDto.Key) != null)
        {
            throw new BaseCustomException(409, "Subscription plan already exist", "Please choose a different subscription plan");
        }
        
        subscription.Key = subscriptionDto.Key.ToUpper();
        subscription.Description = subscriptionDto.Description;
        subscription.UpdatedBy = userId;
        subscription.UpdatedOn = DateTime.UtcNow;

        subscriptionRepository.SaveSubscription(subscription);
        ResponseDto responseDto = new ResponseDto()
        {
            StatusCode = 200,
            Message = "Subscription plan updated successfully",
            Description = "The selected subscription is updated in the database"
        };
        return responseDto;
    }

    
}
