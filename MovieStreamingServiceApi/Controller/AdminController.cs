using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Entity.Dto;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Entity.Data;
using AutoMapper;
using Service;
using Repository;
using Contract.IService;
using NLog;

namespace MovieStreamingServiceApi.Controller
{ 
    
    [ApiController]
    public class AdminController : ControllerBase
    { 
        private readonly IAdminService adminService;
        private readonly IConfiguration _config;
        private readonly ApiDbContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;


        public AdminController(IConfiguration config, ApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            adminService = new AdminService(_config, _context,_mapper);
        }

        /// <summary>
        /// Admin can delete user by using this delete user api
        /// </summary>
        /// <remarks>Admin can delete user from the database</remarks>
        /// <param name="userId">Id of the user to delete</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("/api/delete/user/{user-id}")]
        [Authorize(Roles="Admin,User")]
        [SwaggerOperation("ApiDeleteUserUserIdDelete")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult DeleteUserApi([FromRoute(Name ="user-id")][Required]Guid usersId)
        { 
            Guid usrId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            string role = User.Claims.FirstOrDefault(r => r.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")!.Value;
            return Ok(adminService.DeleteUserByUserId(usrId, role, usersId));
        }

        /// <summary>
        /// Admin can view the list of user account registered by using this get user api
        /// </summary>
        /// <remarks>To get all user list with their profile information</remarks>
        /// <param name="rowSize">number of user to be returned</param>
        /// <param name="startIndex">cursor position to fetch the user</param>
        /// <param name="sortOrder">order to retrieve the user details</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/users")]
        [Authorize(Roles="Admin,User")]
        [SwaggerOperation("ApiUsersGet")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<ProfileDto>), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult GetUserApi([FromQuery]int rowSize = 5, [FromQuery]int startIndex = 1, [FromQuery]string? sortOrder = "asc")
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            string role = User.Claims.FirstOrDefault(r => r.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")!.Value;
            return Ok(adminService.GetAllUser(rowSize, startIndex, sortOrder,userId,role));
        }
    }
}
