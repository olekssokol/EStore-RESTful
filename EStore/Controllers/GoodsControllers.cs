using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
                db.Goods.Add(new Goods { Name = "Mechanical keyboard", Quantity = 100, PriceForOne = 75 });
                db.Goods.Add(new Goods { Name = "Mechanical mouse", Quantity = 55, PriceForOne = 45 });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> Get()
        {
            return await db.Goods.ToListAsync();
        }

        [HttpGet("{word}")]
        public async Task<ActionResult<Goods>> Get(string word)
        {
            int count = 0;
            String names = "";
            if (word != null)
            {
                List<Goods> goodsList = new List<Goods>();
                foreach (var good in db.Goods)
                {

                    if (good.Name.Contains(word))
                    {
                        names += good.Name + ", ";
                        count++;

                    }
                }
                if (goodsList == null)
                    return NotFound();
                return new ObjectResult($"Names: {names}\nCount: {count}");
            }

            return NotFound();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Goods>> Get(int id)
        {
            Goods goods = await db.Goods.FirstOrDefaultAsync(x => x.Id == id);
            if (goods == null)
                return NotFound();
            return new ObjectResult(goods);
        }


        [HttpPost]
        public async Task<ActionResult<Goods>> Post(Goods goods)
        {
            if (goods == null)
            {
                return BadRequest();
            }

            db.Goods.Add(goods);
            await db.SaveChangesAsync();
            return Ok(goods);
        }


        [HttpPut]
        public async Task<ActionResult<Goods>> Put(Goods goods)
        {
            if (goods == null)
            {
                return BadRequest();
            }
            if (!db.Goods.Any(x => x.Id == goods.Id))
            {
                return NotFound();
            }

            db.Update(goods);
            await db.SaveChangesAsync();
            return Ok(goods);
        }

        public Goods Put(Int64 id, int quantity)
        {
            Goods goods = db.Goods.FirstOrDefault(x => x.Id == id);

            if (goods != null && db.Goods.Any(x => x.Id == goods.Id))
            {
                goods.Quantity -= quantity;
                return goods;
            }

            return null;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Goods>> Delete(int id)
        {
            Goods goods = db.Goods.FirstOrDefault(x => x.Id == id);
            if (goods == null)
            {
                return NotFound();
            }
            db.Goods.Remove(goods);
            await db.SaveChangesAsync();
            return Ok(goods);
        }
    }
}
