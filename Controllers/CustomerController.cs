using csharp_crud_api.DataContext;
using csharp_crud_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace csharp_crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly dbContext _context;
        public CustomerController(dbContext context)
        {
            _context = context;
        }

        // GET: api/ Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        // GET: api/ Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var user = await _context.Customers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        // PUT: api/PostCustomer
        [HttpPost]
        public async Task<IActionResult> PostCustomer(Customer customer)
        {
            //if (id != customer.Id)
            //{
            //    return BadRequest();
            //}
            _context.Entry(customer).State = EntityState.Added;
            try
            {
                Customer obj = new Customer()
                {
                    Email = customer.Email,
                    FullName = customer.FullName,
                    Phone = customer.Phone,
                    RegDate = DateTime.Now
                };
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                    throw;
               // }
            }
            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        // dummy method to test the connection
        [HttpGet("hello")]
        public string Test()
        {
            return "Hello World!";
        }
    }
}

