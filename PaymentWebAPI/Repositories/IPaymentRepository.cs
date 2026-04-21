using PaymentWebAPI.Models;

namespace PaymentWebAPI.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment? GetById(int id);
        Payment Create(Payment payment);
        bool IsPaymentSuccessful(int id);
    }
}
