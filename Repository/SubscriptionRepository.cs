using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Entity.Data;
using Contract.IRepository;
using Contract.IService;
using Entity.Model;
using NLog;

namespace Repository;
public class SubscriptionRepository : ISubscriptionRepository
{

    private IConfiguration _config;
    private ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private IMapper _mapper;

    public SubscriptionRepository(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _config = config;
        _context = context;
        _mapper = mapper;
    }

    //To save changes for movie details
    public void SaveSubscription(Subscription subscription)
    {
        _logger.Info("Save changes");
        _context.SaveChanges();
    }

    //To save changes for subscription details
    public void SaveMovie(List<Movie> movie)
    {
        _logger.Info("Save changes");
        _context.SaveChanges();
    }

    //To get subscription details by id
    public Subscription? GetSubscriptionDetailsById(Guid? movieId)
    {
        _logger.Info("Get subscription details from the database by using the movie id");
        return _context.Subscriptions.FirstOrDefault(m => m.Id == movieId && m.IsActive == true);
    }

    //To get all movie details by subscription id
    public List<Movie> GetMovieDetailsBySubscriptionId(Guid? subscriptionId)
    {
        _logger.Info("Getting movie details from the database by using the subscription id");
        return _context.Movies.Where(m => m.SubscriptionId == subscriptionId).ToList();
    }

    //To save new subscription plan to database
    public void CreateSubscription(Subscription subscription)
    {
        _logger.Info("Creating new subscription in the database");
        _context.Subscriptions.Add(subscription);
        _context.SaveChanges();
    }

    //Get subscription id by subscription key
    public Guid? GetSubscriptionIdByKey(string? subscriptionPlan)
    {
        _logger.Info("Getting subscription id from the database by using the subscription plan");
        return _context.Subscriptions.FirstOrDefault(s => s.Key == subscriptionPlan!.ToUpper() && s.IsActive == true)?.Id;
    }

    //Get all subscription details
    public List<Subscription> GetAllSubscriptionDetails(int rowSize, int startIndex, string sortOrder)
    {
        _logger.Info("Getting all subscription details from the database");
        IQueryable<Subscription> query = _context.Subscriptions.Where(s => s.IsActive == true);
        if (sortOrder.ToLower() == "desc")
            query = query.OrderByDescending(o => o.Key);
        else
            query = query.OrderBy(o => o.Key);
        return query.Skip(startIndex - 1 > 0 ? startIndex : 0).Take(rowSize > 0 ? rowSize : 5).ToList();
    }

    //Get user subscription id by userId
    public Guid GetSubscriptionIdByUserId(Guid userId)
    {
        _logger.Info("Getting subscription id from the database using user id");
        return _context.Profiles.FirstOrDefault(k => k.UserId == userId)!.SubscriptionId;
    }
}
