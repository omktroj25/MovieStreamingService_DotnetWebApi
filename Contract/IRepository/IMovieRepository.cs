using Entity.Model;

namespace Contract.IRepository;
public interface IMovieRepository
{
    void SaveMovie(Movie movie);
    Movie? GetMovieDetailsById(Guid? movieId);
    void CreateMovie(Movie movie);
    bool ValidateMovieTitle(string? title);
    Guid? GetSubscriptionIdByKey(string? subscriptionPlan);
    Guid GetSubscriptionIdByUserId(Guid userId);
    List<Movie> GetAllMovies(string role, Guid userId, string? title, string? genere, string? director, string? actor, decimal rating, int rowSize, int startIndex, string? sortBy, string? sortOrder);
    string GetSubscritpionKeyById(Guid subscriptionId);
}
