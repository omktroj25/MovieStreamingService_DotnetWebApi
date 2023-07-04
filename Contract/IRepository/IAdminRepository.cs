using Entity.Model;

namespace Contract.IRepository;
public interface IAdminRepository
{
    User? GetUserByUserId(Guid tempId);
    Entity.Model.Profile GetProfileDetailsByUserId(Guid userId);
    UpiPayment? GetUpiPaymentDetailsByUserId(Guid? userId);
    CardPayment? GetCardPaymentDetailsByUserId(Guid? userId);
    void SaveUpdate(User user, Entity.Model.Profile profile, UpiPayment? upiPayment, CardPayment? cardPayment);
    List<User> GetUsers(int rowSize, int startIndex, string sortOrder);
    string GetSubscritpionKeyById(Guid subscriptionId);
}
