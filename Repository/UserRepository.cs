using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Entity.Data;
using Entity.Model;
using Contract.IRepository;
using Contract.IService;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Repository;
public class UserRepository
{

    private IConfiguration _config;
    private ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private IMapper _mapper;

    public UserRepository(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _config = config;
        _context = context;
        _mapper = mapper;
    }


    //Get user details by using user name
    public User? GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == username && u.IsActive == true);
    }

    //Validate email number conflict
    public bool ValidateEmailConflict(string email)
    {
        return _context.Profiles.Any(e => e.Email == email && e.IsActive == true);
    }

    //Validate phone number conflict
    public bool ValidatePhoneConflict(string phone)
    {
        return _context.Profiles.Any(p => p.PhoneNumber == phone && p.IsActive == true);
    }


    //Get subscription id by subscription key
    public Guid? GetSubscriptionIdByKey(string? subscriptionPlan)
    {
        return _context.Subscriptions.FirstOrDefault(s => s.Key == subscriptionPlan!.ToUpper() && s.IsActive == true)?.Id;
    }

    //Get user details by user id
    public User? GetUserByUserId(Guid tempId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == tempId && u.IsActive == true);
    }

    //Get profile details by user id
    public Entity.Model.Profile GetProfileDetailsByUserId(Guid userId)
    {
        return _context.Profiles.FirstOrDefault(p => p.UserId == userId)!;
    }

    //Get upi payment details by user id
    public UpiPayment? GetUpiPaymentDetailsByUserId(Guid? userId)
    {
        return _context.UpiPayments.FirstOrDefault(u => u.UserId == userId && u.IsActive == true);
    }

    //Get card payment details by user id
    public CardPayment? GetCardPaymentDetailsByUserId(Guid? userId)
    {
        return _context.CardPayments.FirstOrDefault(u => u.UserId == userId && u.IsActive == true);
    }

    //To validate upi id
    public bool ValidateUpiId(string? upiId)
    {
        return _context.UpiPayments.Any(u => u.UpiId == upiId && u.IsActive == true);
    }

    //To validate card number
    public bool ValidateCardNumber(string? cardNumber)
    {
        return _context.CardPayments.Any(u => u.CardNumber == cardNumber && u.IsActive == true);
    }

    //To save user and profile in database
    public void CreateUser(User user, Entity.Model.Profile profile)
    {
        _context.Users.Add(user);
        _context.Profiles.Add(profile);
        _context.SaveChanges();
    }

    //To save upi payment in the database
    public void CreateUpi(UpiPayment upiPayment)
    {
        _context.UpiPayments.Add(upiPayment);
        _context.SaveChanges();
    }

    //To save card payment in the database
    public void CreateCard(CardPayment cardPayment)
    {
        _context.CardPayments.Add(cardPayment);
        _context.SaveChanges();
    }

    //To update user information
    public void SaveUpdate(User user, Entity.Model.Profile profile, UpiPayment? upiPayment, CardPayment? cardPayment)
    {
        _context.SaveChanges();
    }

}
