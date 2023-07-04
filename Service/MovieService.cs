using Entity.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository;
using Contract.IService;
using Contract.IRepository;
using Entity.Dto;
using Entity.Model;
using Exception;
using NLog;

namespace Service;
public class MovieService : IMovieService
{

    private readonly IMovieRepository movieRepository;
    private readonly IConfiguration _config;
    private readonly ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly IMapper _mapper;
        
    public MovieService(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _config = config;
        _context = context;
        movieRepository = new MovieRepository(_config, _context,_mapper);
    }

    //Delete movie by movie id
    public ResponseDto DeleteMovieById(Guid? movieId,Guid userId)
    {   
        Movie? movie = movieRepository.GetMovieDetailsById(movieId);
        if(movie == null)
        {
            _logger.Warn("Movie not found in the database {0}",movieId);
            throw new BaseCustomException(404, "Movie id not found", "Please give the valid movie id");
        }
        
        movie.IsActive = false;
        movie.UpdatedBy = userId;
        movie.UpdatedOn = DateTime.UtcNow;
        
        _logger.Info("Movie status changed as false");
        movieRepository.SaveMovie(movie);
        ResponseDto responseDto = new ResponseDto()
        {
            StatusCode = 200,
            Message = "Movie deleted successfully",
            Description = "The selected movie is removed from the database"
        };
        _logger.Info("Movie deleted successfully");
        return responseDto;
    }

    //Update movie by id
    public ResponseDto UpdateMovieById(Guid? movieId, MovieDto movieDto, Guid userId)
    {
        
        Movie? movie = movieRepository.GetMovieDetailsById(movieId);
        if(movie == null)
        {
            _logger.Warn("Movie details not found {0}",movieId);
            throw new BaseCustomException(404, "Movie id not found", "Please give the valid movie id");
        }
        Guid? subscriptionId = movieRepository.GetSubscriptionIdByKey(movieDto.SubscriptionPlan);
        if (movieRepository.ValidateMovieTitle(movieDto.Title) == true)
        {
            _logger.Warn("Movie details already exist");
            throw new BaseCustomException(409, "Movie title already exist", "movie name already taken. Please type different movie name");
        }
        if (subscriptionId == null)
        {
            _logger.Warn("Subscription plan not found");
            throw new BaseCustomException(404, "Subscription plan not found", "Please choose a different subscription plan");
        }
        
        movie.Title = movieDto.Title;
        movie.Genere = movieDto.Genere;
        movie.Actor = movieDto.Actor;
        movie.Director = movieDto.Director;
        movie.Rating = movieDto.Rating;
        movie.SubscriptionId = (Guid)subscriptionId;
        movie.UpdatedBy = userId;
        movie.UpdatedOn = DateTime.UtcNow;
        
        movieRepository.SaveMovie(movie);
        ResponseDto responseDto = new ResponseDto()
        {
            StatusCode = 200,
            Message = "Movie updated successfully",
            Description = "The selected movie is updated in the database"
        };
        _logger.Info("Movie updated successfully");
        return responseDto;
    }

    //Add new movie to database by admin user
    public ResponseIdDto AddNewMovie(MovieDto movieDto, Guid userId)
    {
        Guid movieId = Guid.NewGuid();
        movieDto.Id = movieId;
        movieDto.UserId = userId;
        movieDto.CreatedBy = userId;
        movieDto.CreatedOn = DateTime.UtcNow;
        Guid? subscriptionId = movieRepository.GetSubscriptionIdByKey(movieDto.SubscriptionPlan);
        if (movieRepository.ValidateMovieTitle(movieDto.Title) == true)
        {
            _logger.Warn("Movie title already exist");
            throw new BaseCustomException(409, "Movie title already exist", "movie name already taken. Please type different movie name");
        }
        if (subscriptionId == null)
        {
            _logger.Warn("Subscription plan not found");
            throw new BaseCustomException(404, "Subscription plan not found", "Please choose a different subscription plan");
        }
        movieDto.SubscriptionId = (Guid)subscriptionId;
        Movie movie = _mapper.Map<Movie>(movieDto);
        movieRepository.CreateMovie(movie);
        ResponseIdDto responseIdDto = new ResponseIdDto()
        {
            Id = movieId,
            Message = "New movie created successfully",
        };
        _logger.Info("New movie created successfully");
        return responseIdDto;
    }

    public List<MovieDto> GetMovieDetails(string role, Guid userId, string? title, string? genere, string? director, string? actor, decimal rating, int rowSize, int startIndex, string? sortBy, string? sortOrder)
    {
        _logger.Info("Get movie api called");
        List<Movie> movies = movieRepository.GetAllMovies(role, userId, title?.ToLower(), genere?.ToLower(), director?.ToLower(), actor?.ToLower(), rating, rowSize, startIndex, sortBy, sortOrder);
        List<MovieDto> movieDto = new List<MovieDto>();
        _logger.Info("Assign movie details to movie dto");
        foreach(Movie movie in movies)
        {
            MovieDto dto = _mapper.Map<MovieDto>(movie);
            dto.SubscriptionPlan = movieRepository.GetSubscritpionKeyById(movie.SubscriptionId);
            movieDto.Add(dto);
        }
        _logger.Info("Movie fetched successfully");
        return movieDto;
    }
}
