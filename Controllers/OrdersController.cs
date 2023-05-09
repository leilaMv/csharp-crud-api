namespace csharp_crud_api.Controllers
{
    using csharp_crud_api.DataContext;
    using csharp_crud_api.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly dbContext _context;
        public OrdersController(dbContext context)
        {
            _context = context;
        }

        [HttpGet("GetOrders")]
        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PostOrder(int customerId)
        {
            if (_context.Customers.Find(customerId) == null)
            {
                return BadRequest();
            }
            var _customer = _context.Customers.Find(customerId);
            try
            {
                Order obj = new Order()
                {
                   
                    Customer = _customer ,
                    OrderDate = DateTime.Now
                };
                _context.Entry(obj).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!OrderExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }
            return NoContent();
        }
        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }

}
