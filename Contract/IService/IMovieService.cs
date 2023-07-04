using Entity.Dto;

namespace Contract.IService;
public interface IMovieService
{
    ResponseDto DeleteMovieById(Guid? movieId,Guid userId);
    ResponseDto UpdateMovieById(Guid? movieId, MovieDto movieDto, Guid userId);
    ResponseIdDto AddNewMovie(MovieDto movieDto, Guid userId);
    List<MovieDto> GetMovieDetails(string role, Guid userId, string? title, string? genere, string? director, string? actor, decimal rating, int rowSize, int startIndex, string? sortBy, string? sortOrder);
}
