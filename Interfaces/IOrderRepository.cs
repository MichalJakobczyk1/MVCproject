using MVCproject.Models;

namespace MVCproject.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetByIdAsync(int id);
        bool Add(Order order);
        bool Save();
    }
}
