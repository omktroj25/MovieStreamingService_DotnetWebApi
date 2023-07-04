using Microsoft.Extensions.Configuration;
using Entity.Data;
using AutoMapper;
using Repository;
using Microsoft.Extensions.Logging;
using Contract.IRepository;
using Contract.IService;
using Microsoft.AspNetCore.Http;
using Entity.Dto;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Entity.Model;
using NLog;
using Exception;

namespace Service;
public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationRepository authenticationRepository;
    private readonly IConfiguration _config;
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


    public AuthenticationService(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _config = config;
        _context = context;
        authenticationRepository = new AuthenticationRepository(_config, _context, _mapper);
    }


    //Generate JWT token with expire and claims to login
    public TokenResponseDto GenerateToken(LoginDto loginDto)
    {
        User user = ValidateUser(loginDto);

        _logger.Info("JWT token generation begins");
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Key"]!));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim("NameId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        _logger.Info("JWT token generated successfully");

        TokenResponseDto tokenResponseDto = new TokenResponseDto
        {
            TokenType = "Bearer Token",
            AccessToken = tokenString,
        };
        _logger.Info("{0}",tokenResponseDto);

        return tokenResponseDto;
    }

    //Validate user name and password
    public User ValidateUser(LoginDto loginDto)
    {
        _logger.Info("Input validation begins");
        string hashedPasswordEntered = ComputeHash(loginDto.Password!);
        User? user = authenticationRepository.GetUserByUsername(loginDto.UserName!);
        if (user == null)
        {
            _logger.Warn("User not found in the database");
            throw new BaseCustomException(401, "Unauthorized access", "Invalid user name. Please check your login details and try again");
        }
        if (loginDto.UserName == user.UserName && user.Password == hashedPasswordEntered)
        {
            _logger.Info("Input validation completed");
            return user;
        }
        _logger.Warn("User not found in the database");
        throw new BaseCustomException(401, "Unauthorized access", "Invalid password. Please check your login details and try again");
    }

    //Hash the input password to validate
    public string ComputeHash(string input)
    {
        _logger.Info("Hashing the password {0}",input);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

}
