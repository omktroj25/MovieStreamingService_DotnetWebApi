using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Entity.Dto;
using Repository;
using AutoMapper;
using Entity.Data;
using NLog;
using Service;

namespace MovieStreamingServiceApi.Controller
{ 
      
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationService authenticationService;
        private readonly IConfiguration _config;
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        
        public AuthenticationController(IConfiguration config, ApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            authenticationService = new AuthenticationService(_config, _context,_mapper);
        }

        /// <summary>
        /// Login api for admin and user
        /// </summary>
        /// <remarks>To login to the api using username and password</remarks>
        /// <param name="body">To send login credentials to the server to validate</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/auth/login")]
        [SwaggerOperation("UserLogin")]
        [SwaggerResponse(statusCode: 200, type: typeof(TokenResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 401, type: typeof(ResponseDto), description: "Unauthorized access")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UserLoginApi([FromBody]LoginDto loginDto)
        {
            _logger.Info("Login api called");
            return Ok(authenticationService.GenerateToken(loginDto));
        }

    }
}
