using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Entity.Dto;
using Entity.Data;
using Service;
using Repository;
using AutoMapper;
using NLog;

namespace MovieStreamingServiceApi.Controller
{ 
    
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService userService;
        private readonly IConfiguration _config;
        private readonly ApiDbContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        
        public UserController(IConfiguration config, ApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            userService = new UserService(_config, _context, _mapper);
        }

        /// <summary>
        /// Register api for new user registration
        /// </summary>
        /// <remarks>User can create account to access movie streaming service by registering their information</remarks>
        /// <param name="body">To send profile information to the server to save in the database</param>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/auth/register")]
        [SwaggerOperation("UserRegisterApi")]
        [SwaggerResponse(statusCode: 201, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UserRegisterApi([FromBody]ProfileDto profileDto)
        {
            return CreatedAtAction(nameof(UserRegisterApi), userService.RegisterUser(profileDto));
        }

        /// <summary>
        /// Admin and user can update user profile using this update profile api
        /// </summary>
        /// <remarks>Update the profile of the user by admin and user</remarks>
        /// <param name="userId">Id of the user to update the profile details</param>
        /// <param name="body">To send profile information to update in the database</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("/api/profile/{user-id}")]
        [Authorize(Roles="Admin,User")]
        [SwaggerOperation("UpdateUserApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UpdateUserApi([FromRoute(Name ="user-id")]Guid? usersId, [FromBody]ProfileDto profileDto)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            string role = User.Claims.FirstOrDefault(r => r.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")!.Value;
            return Ok(userService.UpdateUserDetails(userId, usersId, role, profileDto));
        }
    }
}
