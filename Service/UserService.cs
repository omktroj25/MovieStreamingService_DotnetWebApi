using Entity.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository;
using Contract.IService;
using Contract.IRepository;
using Entity.Dto;
using Exception;
using Entity.Model;
using System.Security.Cryptography;
using System.Text;
using NLog;

namespace Service;
public class UserService
{

    private readonly UserRepository userRepository;
    private readonly IConfiguration _config;
    private readonly ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IMapper _mapper;
        
    public UserService(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _config = config;
        _context = context;
        userRepository = new UserRepository(_config, _context, _mapper);
    }

    //Validate input
    public void ValidateProfileDto(ProfileDto profileDto)
    {
        if(userRepository.GetUserByUsername(profileDto.UserName!) != null)
        {
            throw new BaseCustomException(409, "User name already exists", "Username already taken. Please choose a different username");
        }
        if(userRepository.ValidatePhoneConflict(profileDto.PhoneNumber!) == true)
        {
            throw new BaseCustomException(409, "Phone number already exists", "Phone number already taken. Please choose a different phone number");
        }
        if(userRepository.ValidateEmailConflict(profileDto.EmailAddress!) == true)
        {
            throw new BaseCustomException(409, "Email address already exists", "Email id already taken. Please choose a different email address");
        }
        foreach( ProfileDtoPaymentDto payment in profileDto.PaymentDto!)
        {
            if(userRepository.GetUpiPaymentDetailsByUserId(profileDto.UserId) != null && payment.PaymentType == "UPI")
            {
                if(userRepository.ValidateUpiId(payment.UpiId))
                {
                    throw new BaseCustomException(409, "Upi id already exist", "Please type the new upi id");
                }
            }
            else if(userRepository.GetCardPaymentDetailsByUserId(profileDto.UserId) != null && payment.PaymentType == "CREDIT/CARD" || payment.PaymentType == "DEBIT/CARD")
            {
                if(userRepository.ValidateCardNumber(payment.CardNumber))
                {
                    throw new BaseCustomException(409, "Card number already exist", "Please type the new card number");
                }
            }
        }
        Guid? subscriptionId = userRepository.GetSubscriptionIdByKey(profileDto.SubscriptionPlan);
        if (subscriptionId == null)
        {
            throw new BaseCustomException(404, "Subscription plan not found", "Please choose a different subscription plan");
        }
        profileDto.SubscriptionId = (Guid)subscriptionId;
        
    }

    //Hash password to store in database
    public string ComputeHash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    //Registering new user
    public ResponseIdDto RegisterUser(ProfileDto profileDto)
    {
        if(profileDto.Password != profileDto.ConfirmPassword)
        {
            throw new BaseCustomException(400, "password and confirm password doesn't match", "Please check the input");
        }
        Guid userId = Guid.NewGuid();
        profileDto.Id = Guid.NewGuid();
        profileDto.UserId = userId;
        profileDto.CreatedBy = userId;
        profileDto.CreatedOn = DateTime.UtcNow;
        ValidateProfileDto(profileDto);
        profileDto.Password = ComputeHash(profileDto.Password!);
        User user = _mapper.Map<User>(profileDto);
        Entity.Model.Profile profile = _mapper.Map<Entity.Model.Profile>(profileDto);
        userRepository.CreateUser(user, profile);
        int upicount = 1, cardcount = 1;
        foreach( ProfileDtoPaymentDto payment in profileDto.PaymentDto!)
        {
            
            if(payment.PaymentType == "UPI")
            {
                if(payment.UpiId != null && payment.UpiApp != null && upicount ==1 )
                {
                    payment.Id = Guid.NewGuid();
                    payment.UserId = userId;
                    payment.CreatedBy = userId;
                    payment.CreatedOn = DateTime.UtcNow;
                    UpiPayment upiPayment = _mapper.Map<UpiPayment>(payment);
                    userRepository.CreateUpi(upiPayment);
                    upicount = 0;
                }
            }
            else if(payment.PaymentType == "CREDIT/CARD" || payment.PaymentType == "DEBIT/CARD")
            {
                if(payment.CardNumber != null && payment.CardHolderName != null && payment.ExpireDate != null && cardcount ==1 )
                {
                    payment.Id = Guid.NewGuid();
                    payment.UserId = userId;
                    payment.CreatedBy = userId;
                    payment.CreatedOn = DateTime.UtcNow;
                    CardPayment cardPayment = _mapper.Map<CardPayment>(payment);
                    userRepository.CreateCard(cardPayment);
                    cardcount = 0;
                }
            }
            else
            {
                throw new BaseCustomException(400, "payment method is empty", "Please fill the payment method");
            }
        }
    
        ResponseIdDto responseIdDto = new ResponseIdDto()
        {
            Id = userId,
            Message = "User registered successfully. Go to login page for login"
        };
        return responseIdDto;
    }

    //To select which user id to use
    private Guid SelectUserId(Guid userId, Guid? usersId, string role)
    {
        if(role == "Admin" && usersId == null)
        {
            throw new BaseCustomException(400, "Admin user have to give user id", "Please give user id to update details");
        }
        else if( role == "Admin" && usersId != null)
        {
            return (Guid)usersId!;
        }
        else
        {
            return userId;
        }
    }

    //Update profile for user
    public ResponseDto UpdateUserDetails(Guid userId, Guid? usersId, string role, ProfileDto profileDto)
    {
        Guid tempId = SelectUserId(userId, usersId, role);
        User? user = userRepository.GetUserByUserId(tempId);
        if(user == null)
        {
            throw new BaseCustomException(404, "User id not found", "Please give valid user id to update details");
        }
        if(user.Role == "Admin")
        {
            throw new BaseCustomException(400, "Admin account cant be updated", "Please give valid user id to update details");
        }
        Entity.Model.Profile profile = userRepository.GetProfileDetailsByUserId(tempId);
        UpiPayment? upiPayment = userRepository.GetUpiPaymentDetailsByUserId(tempId);
        CardPayment? cardPayment = userRepository.GetCardPaymentDetailsByUserId(tempId);
        profileDto.UserId = tempId;
        profileDto.Password = ComputeHash(profileDto.Password!);
        ValidateProfileDto(profileDto);

        user.UserName = profileDto.UserName;
        user.Password = profileDto.Password;
        user.UpdatedBy = userId;
        user.UpdatedOn = DateTime.UtcNow;
        profile.Email = profileDto.EmailAddress;
        profile.PhoneNumber = profileDto.PhoneNumber;
        profile.SubscriptionId = profileDto.SubscriptionId;
        profile.UpdatedBy = userId;
        profile.UpdatedOn = DateTime.UtcNow;
        
        foreach( ProfileDtoPaymentDto payment in profileDto.PaymentDto!)
        {
            if(payment.PaymentType == "UPI")
            {
                if(upiPayment != null)
                {
                    upiPayment.PaymentType = payment.PaymentType;
                    upiPayment.UpiId = payment.UpiId;
                    upiPayment.UpiApp = payment.UpiApp;
                    upiPayment.UpdatedBy = userId;
                    upiPayment.UpdatedOn = DateTime.UtcNow;
                }
            }
            if(payment.PaymentType == "CREDIT/CARD" || payment.PaymentType == "DEBIT/CARD")
            {
                if(cardPayment != null)
                {
                    cardPayment.PaymentType = payment.PaymentType;
                    cardPayment.CardNumber = payment.CardNumber;
                    cardPayment.CardHolderName = payment.CardHolderName;
                    cardPayment.ExpireDate = payment.ExpireDate;
                    cardPayment.UpdatedBy = userId;
                    cardPayment.UpdatedOn = DateTime.UtcNow;
                }
            }
        }
        
        userRepository.SaveUpdate(user, profile, upiPayment, cardPayment);
        ResponseDto responseDto = new ResponseDto()
        {
            StatusCode = 200,
            Message = "User details updated successfully",
            Description = "The user informations are updated in the database"
        };
        return responseDto;
    }

}
