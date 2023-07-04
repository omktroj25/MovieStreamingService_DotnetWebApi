using Entity.Model;

namespace Contract.IRepository;
public interface IAuthenticationRepository
{
    User? GetUserByUsername(string username);
}
