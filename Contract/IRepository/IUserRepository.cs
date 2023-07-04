using Entity.Model;

namespace Contract.IRepository;
public interface IUserRepository
{
    User? GetUserByUsername(string username);
    bool ValidateEmailConflict(string email);
    bool ValidatePhoneConflict(string phone);
    Guid? GetSubscriptionIdByKey(string? subscriptionPlan);
    User? GetUserByUserId(Guid tempId);
    Entity.Model.Profile GetProfileDetailsByUserId(Guid userId);
    UpiPayment? GetUpiPaymentDetailsByUserId(Guid? userId);
    CardPayment? GetCardPaymentDetailsByUserId(Guid? userId);
    bool ValidateUpiId(string? upiId);
    bool ValidateCardNumber(string? cardNumber);
    void CreateUser(User user, Entity.Model.Profile profile);
    void CreateUpi(UpiPayment upiPayment);
    void CreateCard(CardPayment cardPayment);
    void SaveUpdate(User user, Entity.Model.Profile profile, UpiPayment? upiPayment, CardPayment? cardPayment);
}
