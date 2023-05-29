using Entity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Entity.Model;
using Service;
using Repository;
using Contract.IService;
using Contract.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MovieStreamingServiceApi;
using MovieStreamingServiceApi.Controller;
using Microsoft.Extensions.DependencyInjection;

namespace MovieStreamingServiceApiTest
{
    public class AdminBaseTesting : BaseTesting
    {
        public AdminBaseTesting() : base()
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim("NameIdentifier", name),
                new Claim("NameId",userId.ToString()),
                new Claim(ClaimTypes.Role, "Admin" ),
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "TestAuthType");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            DefaultHttpContext httpContext = new DefaultHttpContext();
            httpContext.User = principal;
            ControllerContext controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
            adminController.ControllerContext = controllerContext;
            authenticationController.ControllerContext = controllerContext;
            movieController.ControllerContext = controllerContext;
            subscriptionController.ControllerContext = controllerContext;
            userController.ControllerContext = controllerContext;

        }

    }
}
