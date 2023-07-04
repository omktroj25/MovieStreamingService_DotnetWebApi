using Entity.Dto;

namespace Contract.IService;
public interface IAdminService
{
    ResponseDto DeleteUserByUserId(Guid usrId, string role, Guid usersId);
    List<ProfileDto> GetAllUser(int rowSize, int startIndex, string? sortOrder, Guid userId, string role);   
}
