using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStreamingServiceApi.Middleware.JwtAuthenticationMiddleware;
public static class TokenBlacklist
{
    private static List<string> _blacklist = new List<string>();

    public static void AddToken(string token)
    {
        _blacklist.Add(token);
    }

    public static bool IsTokenBlacklisted(string token)
    {
        return _blacklist.Contains(token);
    }
}