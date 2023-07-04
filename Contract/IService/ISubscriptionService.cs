using Entity.Dto;

namespace Contract.IService;
public interface ISubscriptionService
{
    ResponseDto DeleteSubscriptionById(Guid? subscriptionId, Guid userId);
    List<SubscriptionDto> GetSubscriptionDetails(string role, Guid userId, int rowSize, int startIndex, string sortOrder);
    ResponseIdDto AddNewSubscription(SubscriptionDto subscriptionDto,Guid userId);
    ResponseDto UpdateSubscriptionById(Guid? subscriptionId, SubscriptionDto subscriptionDto, Guid userId);
}
