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
using AutoMapper;
using Service;
using NLog;
using Repository;
using Contract.IService;
using Contract.IRepository;

namespace MovieStreamingServiceApi.Controller
{ 
   
    [ApiController]
    public class MovieController : ControllerBase
    { 

        private readonly IMovieService movieService;
        private readonly IConfiguration _config;
        private readonly ApiDbContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        
        public MovieController(IConfiguration config, ApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _context = context;
            movieService = new MovieService(_config, _context, _mapper);
        }

        /// <summary>
        /// Admin can delete movie by using this delete movie api
        /// </summary>
        /// <remarks>Admin can remove movie details from the database</remarks>
        /// <param name="movieId">Id of the movie to delete</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("/api/delete/movie/{movie-id}")]
        [Authorize(Roles="Admin")]
        [SwaggerOperation("DeleteMovieApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult DeleteMovieApi([FromRoute(Name ="movie-id")][Required]Guid? movieId)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(movieService.DeleteMovieById(movieId,userId));
        }

        /// <summary>
        /// Admin can update movie details using this update movie api
        /// </summary>
        /// <remarks>Admin can update movie details using this api</remarks>
        /// <param name="movieId">Id of the movie to update the information</param>
        /// <param name="body">To send movie details to update in the database</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [Route("/api/movie/{movie-id}")]
        [Authorize(Roles="Admin")]
        [SwaggerOperation("UpdateMovieApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(ResponseDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 404, type: typeof(ResponseDto), description: "Not found")]
        [SwaggerResponse(statusCode: 409, type: typeof(ResponseDto), description: "Conflict")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult UpdateMovieApi([FromRoute(Name ="movie-id")][Required]Guid? movieId, [FromBody]MovieDto movieDto)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return Ok(movieService.UpdateMovieById(movieId, movieDto, userId));
        }

        /// <summary>
        /// Admin can add movie using this add movie api
        /// </summary>
        /// <remarks>New movie details can be added to the database</remarks>
        /// <param name="body">To add new movie details to the database</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Route("/api/movie")]
        [Authorize(Roles="Admin")]
        [SwaggerOperation("AddMovieApi")]
        [SwaggerResponse(statusCode: 201, type: typeof(ResponseIdDto), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult AddMovieApi([FromBody]MovieDto movieDto)
        { 
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            return CreatedAtAction(nameof(AddMovieApi),movieService.AddNewMovie(movieDto, userId));
        }

        /// <summary>
        /// User can view list of movie according to his subscription plan and admin can view all movies in the database
        /// </summary>
        /// <remarks>To get all movie list with their details</remarks>
        /// <param name="rowSize">number of movie to be returned</param>
        /// <param name="startIndex">cursor position to fetch the movie</param>
        /// <param name="sortOrder">order to retrieve the movie details</param>
        /// <param name="title">retrieve the movie based on the title</param>
        /// <param name="genere">retrieve the movie based on the genere</param>
        /// <param name="director">retrieve the movie based on the director</param>
        /// <param name="actor">retrieve the movie based on the actor</param>
        /// <param name="rating">order to retrieve the movie details</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("/api/movies")]
        [Authorize(Roles="Admin,User")]
        [SwaggerOperation("GetMovieApi")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<MovieDto>), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(ResponseDto), description: "Bad request")]
        [SwaggerResponse(statusCode: 500, type: typeof(ResponseDto), description: "Internal server error")]
        public virtual IActionResult GetMoviesApi([FromQuery]string? title=null, [FromQuery]string? genere=null, [FromQuery]string? director=null, [FromQuery]string? actor=null, [FromQuery]Decimal rating = 0,[FromQuery]int rowSize = 10, [FromQuery]int startIndex = 1, [FromQuery]string? sortBy = "title",[FromQuery]string? sortOrder = "asc")
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(u => u.Type == "NameId")!.Value);
            string role = User.Claims.FirstOrDefault(r => r.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")!.Value;
            return Ok(movieService.GetMovieDetails(role, userId, title, genere, director, actor, rating, rowSize, startIndex, sortBy, sortOrder));
        }
    }
}
