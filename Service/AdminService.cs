using Entity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository;
using AutoMapper;
using Contract.IRepository;
using Contract.IService;
using Entity.Dto;
using Exception;
using Entity.Model;
using NLog;

namespace Service;
public class AdminService
{
    private readonly AdminRepository adminRepository;
    private readonly IConfiguration _config;
    private readonly ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IMapper _mapper;

    public AdminService(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _config = config;
        _context = context;
        adminRepository = new AdminRepository(_config, _context, _mapper);
    }

    public ResponseDto DeleteUserByUserId(Guid usrId, string role, Guid usersId)
    {
        Guid userId;
        if(role == "User")
        {
            userId = usrId;
        }
        else
        {
            userId = usersId;
        }

        User? user = adminRepository.GetUserByUserId(userId);
        if (user == null)
        {
            throw new BaseCustomException(404, "User id not found", "Please give valid user id to update details");
        }
        else if(user.Role == "Admin")
        {
            throw new BaseCustomException(400, "Admin account cannot be deleted", "Please give the valid user id to delete");
        }
        Entity.Model.Profile profile = adminRepository.GetProfileDetailsByUserId(userId);
        UpiPayment? upiPayment = adminRepository.GetUpiPaymentDetailsByUserId(userId);
        CardPayment? cardPayment = adminRepository.GetCardPaymentDetailsByUserId(userId);

        user.IsActive = false;
        user.UpdatedBy = userId;
        user.UpdatedOn = DateTime.UtcNow;
        profile.IsActive = false;
        profile.UpdatedBy = userId;
        profile.UpdatedOn = DateTime.UtcNow;
        if (upiPayment != null)
        {
            upiPayment.IsActive = false;
            upiPayment.UpdatedBy = userId;
            upiPayment.UpdatedOn = DateTime.UtcNow;
        }
        if (cardPayment != null)
        {
            cardPayment.IsActive = false;
            cardPayment.UpdatedBy = userId;
            cardPayment.UpdatedOn = DateTime.UtcNow;
        }

        adminRepository.SaveUpdate(user, profile, upiPayment, cardPayment);
        ResponseDto responseDto = new ResponseDto()
        {
            StatusCode = 200,
            Message = "User details deleted successfully",
            Description = "The user informations are deleted from the database"
        };
        return responseDto;
    }

    //Get all user form the database according to the given parameters
    public List<ProfileDto> GetAllUser(int rowSize, int startIndex, string? sortOrder, Guid userId, string role)
    {
        List<User> users=new List<User>();
        if(role == "Admin")
        {
            users = adminRepository.GetUsers(rowSize, startIndex, sortOrder!);
        }
        else
        {
            users.Add(adminRepository.GetUserByUserId(userId)!);
        }
        List<ProfileDto> profileDto = new List<ProfileDto>();
        foreach (var user in users)
        {
            ProfileDto dto = AssignToDto(user);
            profileDto.Add(dto);
        }
        return profileDto;
    }

    //Assign user details to the dto
    public ProfileDto AssignToDto(User user)
    {
        ProfileDto dto = new ProfileDto();
        Entity.Model.Profile profile = adminRepository.GetProfileDetailsByUserId(user.Id);
        UpiPayment? upiPayment = adminRepository.GetUpiPaymentDetailsByUserId(user.Id);
        CardPayment? cardPayment = adminRepository.GetCardPaymentDetailsByUserId(user.Id);
        dto.PaymentDto = new List<ProfileDtoPaymentDto>();

        dto.Id = user.Id;
        dto.UserName = user.UserName;
        dto.Password = user.Password;
        dto.ConfirmPassword = user.Password;
        dto.EmailAddress = profile.Email;
        dto.PhoneNumber = profile.PhoneNumber;
        dto.SubscriptionPlan = adminRepository.GetSubscritpionKeyById(profile.SubscriptionId);
        if (upiPayment != null)
        {
            dto.PaymentDto.Add(new ProfileDtoPaymentDto()
            {
                Id = upiPayment.Id,
                PaymentType = upiPayment.PaymentType,
                UpiId = upiPayment.UpiId,
                UpiApp = upiPayment.UpiApp,
            });
        }
        if (cardPayment != null)
        {
            dto.PaymentDto.Add(new ProfileDtoPaymentDto()
            {
                Id = cardPayment.Id,
                PaymentType = cardPayment.PaymentType,
                CardNumber = cardPayment.CardNumber,
                CardHolderName = cardPayment.CardHolderName,
                ExpireDate = cardPayment.ExpireDate
            });

        }
        return dto;
    }
}
