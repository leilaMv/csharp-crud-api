using csharp_crud_api.Models;

namespace csharp_crud_api.Infrastructure
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrders();
        public Task<Order> GetOrder(int Id);
    }
}
