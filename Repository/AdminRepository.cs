using Entity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Contract.IService;
using Contract.IRepository;
using Entity.Model;
using NLog;

namespace Repository;
public class AdminRepository : IAdminRepository
{
    private IConfiguration _config;
    private ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private IMapper _mapper;

    public AdminRepository(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _config = config;
        _context = context;
        _mapper = mapper;
    }

    //Get user details by user id
    public User? GetUserByUserId(Guid tempId)
    {
        _logger.Info("Getting user details from the database by user id");
        return _context.Users.FirstOrDefault(u => u.Id == tempId && u.IsActive == true);
    }

    //Get profile details by user id
    public Entity.Model.Profile GetProfileDetailsByUserId(Guid userId)
    {
        _logger.Info("Getting profile details from the database by user id");
        return _context.Profiles.FirstOrDefault(p => p.UserId == userId)!;
    }

    //Get upi payment details by user id
    public UpiPayment? GetUpiPaymentDetailsByUserId(Guid? userId)
    {
        _logger.Info("Getting upi payment details from the database by user id");
        return _context.UpiPayments.FirstOrDefault(u => u.UserId == userId && u.IsActive == true);
    }

    //Get card payment details by user id
    public CardPayment? GetCardPaymentDetailsByUserId(Guid? userId)
    {
        _logger.Info("Getting card payment details from the database by user id");
        return _context.CardPayments.FirstOrDefault(u => u.UserId == userId && u.IsActive == true);
    }
    //To update user information
    public void SaveUpdate(User user, Entity.Model.Profile profile, UpiPayment? upiPayment, CardPayment? cardPayment)
    {
        _logger.Info("Saving the changes");
        _context.SaveChanges();
    }

    //Get list of user and their details
    public List<User> GetUsers(int rowSize, int startIndex, string sortOrder)
    {
        _logger.Info("Get all user details from the database");
        IQueryable<User> query = _context.Users.Where(u => u.IsActive && u.Role == "User");

        if (sortOrder == "asc")
        {
            query = query.OrderBy(u => u.UserName);
        }
        else if (sortOrder == "desc")
        {
            query = query.OrderByDescending(u => u.UserName);
        }
        else
        {
            query = query.OrderBy(u => u.UserName);
        }
        return query.Skip(startIndex - 1 > 0 ? startIndex : 0).Take(rowSize > 0 ? rowSize : 5).ToList();
    }

    //Get subscription key by using the subscription id
    public string GetSubscritpionKeyById(Guid subscriptionId)
    {
        _logger.Info("Getting subscription key by using the subscription id");
        string key= _context.Subscriptions.Where(s => s.Id == subscriptionId).Select(s => s.Key).First();
        return key;
    }
    
}
