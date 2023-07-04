using Entity.Dto;

namespace Contract.IService;
public interface IUserService
{
    void ValidateProfileDto(ProfileDto profileDto);
    ResponseIdDto RegisterUser(ProfileDto profileDto);
    ResponseDto UpdateUserDetails(Guid userId, Guid? usersId, string role, ProfileDto profileDto);
}
