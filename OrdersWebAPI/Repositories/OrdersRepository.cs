using OrdersWebAPI.Models;

namespace OrdersWebAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();
        private int _nextId = 1;

        public IEnumerable<Order> GetAll() => _orders;

        public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

        public Order Create(Order order)
        {
            order.Id = _nextId++;
            _orders.Add(order);
            return order;
        }
    }
}
