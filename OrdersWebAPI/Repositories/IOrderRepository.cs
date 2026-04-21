using OrdersWebAPI.Models;

namespace OrdersWebAPI.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        Order Create(Order order);
    }
}
