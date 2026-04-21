namespace OrdersWebAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public decimal Total { get; set; }
    }
}
