using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Entity.Dto;
using Service;
using Entity.Data;
using AutoMapper;
using Repository;
using NLog;
using Contract.IService;

namespace MovieStreamingServiceApi.Controller
{ 
    
    [ApiController]
    public class SubscriptionController : ControllerBase
    { 

        private readonly ISubscriptionService subscriptionService;
        private readonly IConfiguration _config;
        private readonly ApiDbContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        
        public SubscriptionController(IConfiguration config, ApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            subscriptionService = new SubscriptionService(_config, _context, _mapper);
        }

        /// <summary>
        /// Admin can delete subscription by using this delete subscription api
        /// </summary>
        /// <remarks>Admin can delete user from the database</remarks>
        /// <param name="subscriptionId">Id of the subscription to delete</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("/api/delete/subscription/{subscription-id}")]
        [Authorize(Roles="Admin")]
        [SwaggerOperation("DeleteSubscriptionApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult DeleteSubscriptionApi([FromRoute(Name ="subscription-id")][Required]Guid? subscriptionId)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(subscriptionService.DeleteSubscriptionById(subscriptionId, userId));
        }

        /// <summary>
        /// User can view the subscription plan subscribed and admin can view list of subscription plan available
        /// </summary>
        /// <remarks>To view the subscription details according to the login role</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/subscriptions")]
        [Authorize(Roles="Admin,User")]
        [SwaggerOperation("GetSubscriptionApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<SubscriptionDto>), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult GetSubscriptionApi([FromQuery]int rowSize = 5, [FromQuery]int startIndex = 0, [FromQuery]string? sortOrder = "asc")
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            string role = User.Claims.FirstOrDefault(r => r.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")!.Value;
            return Ok(subscriptionService.GetSubscriptionDetails(role, userId, rowSize, startIndex, sortOrder!));
        }

        /// <summary>
        /// Admin can add subscription plan by using this add subscription api
        /// </summary>
        /// <remarks>New subscription plans can be added to the database</remarks>
        /// <param name="body">To add new subscription plan to the database</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/subscription")]
        [Authorize(Roles="Admin")]
        [SwaggerOperation("AddSubscriptionApi")]
        [SwaggerResponse(statusCode: 201, type: typeof(ResponseIdDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult AddSubscriptionApi([FromBody]SubscriptionDto subscriptionDto)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return CreatedAtAction(nameof(AddSubscriptionApi),subscriptionService.AddNewSubscription(subscriptionDto,userId));
        }

        /// <summary>
        /// Admin can update subscription plan by using this subscription update api
        /// </summary>
        /// <remarks>Admin can update subscription information through this api</remarks>
        /// <param name="subscriptionId">Id of the subscription to update the information</param>
        /// <param name="body">To send subscription information to update in the database</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("/api/subscription/{subscription-id}")]
        [Authorize(Roles="Admin")]
        [SwaggerOperation("UpdateSubscriptionApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UpdateSubscriptionApi([FromRoute(Name ="subscription-id")][Required]Guid? subscriptionId, [FromBody]SubscriptionDto subscriptionDto)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(subscriptionService.UpdateSubscriptionById(subscriptionId,subscriptionDto,userId));
        }
    }
}
