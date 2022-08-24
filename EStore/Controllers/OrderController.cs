using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ApplicationContext db;
        public OrderController(ApplicationContext context)
        {
            db = context;
            if (!db.Order.Any())
            {
                db.Order.Add(new Order { OrderNumber  = 123, UserId = 1 });
                db.Order.Add(new Order { OrderNumber  = 124, UserId = 2 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await db.Order.ToListAsync();
        }

       


        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order order = await db.Order.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }


        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            db.Order.Add(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }


        [HttpPut]
        public async Task<ActionResult<Order>> Put(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!db.Order.Any(x => x.Id == order.Id))
            {
                return NotFound();
            }

            db.Update(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> Delete(int id)
        {
            Order order = db.Order.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            db.Order.Remove(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }
    }
}
