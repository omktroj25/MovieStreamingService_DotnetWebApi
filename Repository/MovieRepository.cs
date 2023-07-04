using Entity.Data;
using Entity.Dto;
using Contract.IRepository;
using Contract.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Entity.Model;
using NLog;

namespace Repository;
public class MovieRepository : IMovieRepository
{

    private IConfiguration _config;
    private ApiDbContext _context;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private IMapper _mapper;

    public MovieRepository(IConfiguration config, ApiDbContext context, IMapper mapper)
    {
        _config = config;
        _context = context;
        _mapper = mapper;
    }

    //To save changes for movie details
    public void SaveMovie(Movie movie)
    {
        _logger.Info("Saving changes for movie {0}",movie.Title);
        _context.SaveChanges();
    }

    //To get movie details by id
    public Movie? GetMovieDetailsById(Guid? movieId)
    {
        _logger.Info("Getting movie details by using movie id {0}",movieId);
        return _context.Movies.FirstOrDefault(m => m.Id == movieId && m.IsActive == true);
    }

    //To save new movie to database
    public void CreateMovie(Movie movie)
    {
        _logger.Info("Creating new movie in the database for {0}",movie.Title);
        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    //Validate movie title
    public bool ValidateMovieTitle(string? title)
    {
        _logger.Info("Validate movie title in the database {0}",title);
        return _context.Movies.Any(m => m.Title == title && m.IsActive == true);
    }

    //Get subscription id by subscription key
    public Guid? GetSubscriptionIdByKey(string? subscriptionPlan)
    {
        _logger.Info("Get subscription id from the database by subscription plan {0}",subscriptionPlan);
        return _context.Subscriptions.FirstOrDefault(s => s.Key == subscriptionPlan!.ToUpper() && s.IsActive == true)?.Id;
    }

    //Get user subscription id by userId
    public Guid GetSubscriptionIdByUserId(Guid userId)
    {
        _logger.Info("Get subscription id from the database by using user id ",userId);
        return _context.Profiles.FirstOrDefault(k => k.UserId == userId)!.SubscriptionId;
    }

    //Get all movies with filter
    public List<Movie> GetAllMovies(string role, Guid userId, string? title, string? genere, string? director, string? actor, decimal rating, int rowSize, int startIndex, string? sortBy, string? sortOrder)
    {
        _logger.Info("Getting all movie details from the database");
        IQueryable<Movie> query;
        if (role == "User")
        {
            query = _context.Set<Movie>()
                .Where(m => m.Title.ToLower().Contains(title ?? "") && m.Genere.ToLower().Contains(genere ?? "") &&
                                m.Director.ToLower().Contains(director ?? "") && m.Actor.ToLower().Contains(actor ?? "") &&
                                    m.Rating >= rating && m.IsActive == true && m.SubscriptionId == GetSubscriptionIdByUserId(userId));
        }
        else
        {
            query = _context.Set<Movie>()
                .Where(m => m.Title.ToLower().Contains(title ?? "") && m.Genere.ToLower().Contains(genere ?? "") &&
                                m.Director.ToLower().Contains(director ?? "") && m.Actor.ToLower().Contains(actor ?? "") &&
                                    m.Rating >= rating && m.IsActive == true);
        }

        switch (sortBy!.ToLower())
        {
            case "title":
                if (sortOrder!.ToLower() == "asc")
                    query = query.OrderBy(q => q.Title);
                else
                    query = query.OrderByDescending(q => q.Title);
                break;

            case "genere":
                if (sortOrder!.ToLower() == "asc")
                    query = query.OrderBy(q => q.Genere);
                else
                    query = query.OrderByDescending(q => q.Genere);
                break;

            case "director":
                if (sortOrder!.ToLower() == "asc")
                    query = query.OrderBy(q => q.Actor);
                else
                    query = query.OrderByDescending(q => q.Actor);
                break;

            case "actor":
                if (sortOrder!.ToLower() == "asc")
                    query = query.OrderBy(q => q.Director);
                else
                    query = query.OrderByDescending(q => q.Director);
                break;

            case "rating":
                if (sortOrder!.ToLower() == "asc")
                    query = query.OrderBy(q => q.Rating);
                else
                    query = query.OrderByDescending(q => q.Rating);
                break;

            default:
                query = query.OrderBy(q => q.Title);
                break;
        }
        return query.Skip(startIndex - 1 > 0 ? startIndex : 0).Take(rowSize > 0 ? rowSize : 10).ToList();

    }

    //Get subscription key by using the subscription id
    public string GetSubscritpionKeyById(Guid subscriptionId)
    {
        _logger.Info("Get subscription key from the database by using the subscription id");
        string key= _context.Subscriptions.Where(s => s.Id == subscriptionId).Select(s => s.Key).First();
        return key;
    }

}
