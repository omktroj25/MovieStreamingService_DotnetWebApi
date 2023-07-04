using Entity.Dto;

namespace Contract.IService;
public interface IAuthenticationService
{
    TokenResponseDto GenerateToken(LoginDto loginDto);
}
