using Entity.Model;

namespace Contract.IRepository;
public interface ISubscriptionRepository
{
    void SaveSubscription(Subscription subscription);
    void SaveMovie(List<Movie> movie);
    Subscription? GetSubscriptionDetailsById(Guid? movieId);
    List<Movie> GetMovieDetailsBySubscriptionId(Guid? subscriptionId);
    void CreateSubscription(Subscription subscription);
    Guid? GetSubscriptionIdByKey(string? subscriptionPlan);
    List<Subscription> GetAllSubscriptionDetails( int rowSize, int startIndex, string sortOrder);
    Guid GetSubscriptionIdByUserId(Guid userId);
}
