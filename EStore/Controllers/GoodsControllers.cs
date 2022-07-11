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
    public class GoodsController : ControllerBase
    {
        ApplicationContext db;
        public GoodsController(ApplicationContext context)
        {
            db = context;
            if (!db.Goods.Any())
            {
                db.Goods.Add(new Goods { Name  = "Mechanical keyboard", Quantity = 100, PriceForOne = 75 });
                db.Goods.Add(new Goods { Name  = "Mechanical mouse", Quantity = 55, PriceForOne = 45 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> Get()
        {
            return await db.Goods.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Goods>> Get(int id)
        {
            Goods order = await db.Goods.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }


        [HttpPost]
        public async Task<ActionResult<Goods>> Post(Goods order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            db.Goods.Add(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }


        [HttpPut]
        public async Task<ActionResult<Goods>> Put(Goods order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!db.Goods.Any(x => x.Id == order.Id))
            {
                return NotFound();
            }

            db.Update(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Goods>> Delete(int id)
        {
            Goods order = db.Goods.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            db.Goods.Remove(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }
    }
}
