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
    public class OrdersController : ControllerBase
    {
        ApplicationContext db;
        public OrdersController(ApplicationContext context)
        {
            /*db = context;
            if (!db.Orders.Any())
            {
                db.Orders.Add(new Orders {  });
                db.Orders.Add(new Orders { OrdersNumber  = 124, UserId = 5 });
                db.SaveChanges();
            }*/
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> Get()
        {
            return await db.Orders.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> Get(int id)
        {
            Orders orders = await db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orders == null)
                return NotFound();
            return new ObjectResult(orders);
        }


        [HttpPost]
        public async Task<ActionResult<Orders>> Post(Orders orders)
        {
            if (orders == null)
            {
                return BadRequest();
            }

            db.Orders.Add(orders);
            await db.SaveChangesAsync();
            return Ok(orders);
        }


        [HttpPut]
        public async Task<ActionResult<Orders>> Put(Orders orders)
        {
            if (orders == null)
            {
                return BadRequest();
            }
            if (!db.Orders.Any(x => x.Id == orders.Id))
            {
                return NotFound();
            }

            db.Update(orders);
            await db.SaveChangesAsync();
            return Ok(orders);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> Delete(int id)
        {
            Orders orders = db.Orders.FirstOrDefault(x => x.Id == id);
            if (orders == null)
            {
                return NotFound();
            }
            db.Orders.Remove(orders);
            await db.SaveChangesAsync();
            return Ok(orders);
        }
    }
}

