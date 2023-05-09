using csharp_crud_api.Models;

namespace csharp_crud_api.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = new List<Order>
    {
        new Order
        {
            Id = 1,
            //ProductId = 1,
            //CustomerId = 2,
            OrderQuantity = 10,
            UnitPrice = 12500.00m,
            Amount = 125000.00m,
            OrderDate = DateTime.Now
        },
        new Order
        {
            Id = 2,
            //ProductId = 2,
            //CustomerId = 1,
            OrderQuantity = 20,
            UnitPrice = 10000.00m,
            Amount = 200000.00m,
            OrderDate = DateTime.Now
        },
        new Order
        {
            Id = 3,
            //ProductId = 3,
            //CustomerId = 3,
            OrderQuantity = 50,
            UnitPrice = 15000.00m,
            Amount = 750000.00m,
            OrderDate = DateTime.Now
        }
    };
        public async Task<List<Order>> GetOrders()
        {
            return await Task.FromResult(orders);
        }
        public async Task<Order?> GetOrder(int Id)
        {
            return await Task.FromResult(orders.FirstOrDefault(x => x.Id == Id));
        }
    }

}
