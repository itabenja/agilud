using PaymentWebAPI.Models;

namespace PaymentWebAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly List<Payment> _payments = new();
        private int _nextId = 1;

        public IEnumerable<Payment> GetAll() => _payments;

        public Payment? GetById(int id) => _payments.FirstOrDefault(p => p.Id == id);

        public Payment Create(Payment payment)
        {
            payment.Id = _nextId++;
            payment.Status = payment.Amount > 0 ? "Success" : "Failed";
            _payments.Add(payment);
            return payment;
        }

        public bool IsPaymentSuccessful(int id)
        {
            var payment = GetById(id);
            return payment != null && payment.Status == "Success";
        }
    }
}
