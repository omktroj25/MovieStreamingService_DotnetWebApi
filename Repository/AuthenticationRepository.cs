using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Entity.Data;
using Entity.Model;
using Contract.IRepository;
using NLog;

namespace Repository;
public class AuthenticationRepository : IAuthenticationRepository
{

    private IConfiguration _config;
    private ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private IMapper _mapper;

    public AuthenticationRepository(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _config = config;
        _context = context;
        _mapper = mapper;
    }

    //Get user details by using user name
    public User? GetUserByUsername(string username)
    {
        _logger.Info("Getting user details by username {0}",username);
        return _context.Users.FirstOrDefault(u => u.UserName == username && u.IsActive == true);
    }
    
}
