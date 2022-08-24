using EStore.Data;
using EStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class OrdersController : ControllerBase
    {
        ApplicationContext db;
        public OrdersController(ApplicationContext context)
        {
            db = context;
            if (!db.Orders.Any())
            {
                Orders orders = new Orders { OrderId = 1, GoodsId = 1, Quantity = 3 };

                orders.TotalPrice = orders.Quantity *
                     (from goods in db.Goods
                      where goods.Id == orders.GoodsId
                      select goods.PriceForOne).First();

                db.Orders.Add(orders);

                GoodsController goodsController = new GoodsController(context);

                Goods good = goodsController.Put(orders.GoodsId, orders.Quantity);

                db.Update(good);

                orders = new Orders { OrderId = 1, GoodsId = 2, Quantity = 2 };

                orders.TotalPrice = orders.Quantity *
                     (from goods in db.Goods
                      where goods.Id == orders.GoodsId
                      select goods.PriceForOne).First();

                db.Orders.Add(orders);

                goodsController = new GoodsController(context);

                good = goodsController.Put(orders.GoodsId, orders.Quantity);

                db.Update(good);

                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> Get(string user, int userId)
        {
            try
            {
                if (user == "user")
                {
                    List<Orders> ordersList = new List<Orders>();
                    List<Orders> ordersResultList = new List<Orders>();
                    Int64 averagePrice = 0;
                    int count = 0;

                    foreach (var orders in db.Orders)
                    {
                        ordersList.Add(orders);
                    }

                    foreach (var order in db.Order)
                    {
                        if (order.UserId == userId)
                        {
                            foreach (var orders in ordersList)
                            {
                                if (order.Id == orders.OrderId)
                                {
                                    averagePrice += orders.TotalPrice;
                                    count++;
                                }
                            }
                        }

                    }
                    if (count != 0)
                    {
                        averagePrice /= count;
                    }

                    return new ObjectResult($"Average price: {averagePrice}, orders count: {count}");
                }

                return await db.Orders.ToListAsync();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Orders>> Get(int id)
        {
            Orders orders = await db.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orders == null)
                return NotFound();
            return new ObjectResult(orders);
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Orders>>> Get(string search)
        {
            if (search == "quantity")
            {
                List<Orders> quantityOrdersList = new List<Orders>();
                foreach (var orders in db.Orders)
                {
                    if (orders.Quantity >= 5 && orders.Quantity <= 10)
                    {
                        quantityOrdersList.Add(orders);
                    }

                }
                return new ObjectResult(quantityOrdersList);
            }
            else if (search == "various goods")
            {
                List<Int64> listId = new List<Int64>();
                List<Orders> ordersList = new List<Orders>();

                foreach (var orders in db.Orders)
                {
                    ordersList.Add(orders);
                }


                foreach (var orders in ordersList)
                {
                    if (!listId.Contains(orders.OrderId))
                    {
                        var ord = ordersList.FirstOrDefault(x => x.OrderId == orders.OrderId && x.GoodsId != orders.GoodsId);

                        if (ord != null)
                        {
                            listId.Add(orders.OrderId);
                        }
                    }

                }

                var ordersWithDifferentProd = from order in ordersList
                                              where listId.Contains(order.OrderId)
                                              select order;

                foreach (var orderId in listId)
                {
                    foreach (var order in ordersWithDifferentProd)
                    {
                        if (order.OrderId == orderId)
                        {
                            ordersWithDifferentProd.FirstOrDefault(o => o == order).Order = db.Order.FirstOrDefault(o => o.Id == orderId);
                        }
                    }
                }

                List<JsonObject> jsonObjects = new List<JsonObject>();

                foreach (var orders in ordersWithDifferentProd)
                {


                    bool searchJson = false;

                    foreach (var jsonObj in jsonObjects)
                    {
                        if (jsonObj.UserId == orders.Order.UserId)
                        {
                            jsonObj.OrdersInfo.Sum += orders.TotalPrice;
                            searchJson = true;
                            break;
                        }
                    }

                    if (!searchJson)
                    {

                        JsonObject obj = new JsonObject
                        {
                            UserId = orders.Order.UserId,
                            OrdersInfo = new OrdersInfo
                            {
                                Sum = orders.TotalPrice,
                                OrderNumber = orders.OrderId
                            }

                        };

                        jsonObjects.Add(obj);
                    }
                }

                return new ObjectResult(jsonObjects);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Orders>> Post(Orders orders)
        {
            if (orders == null)
            {
                return BadRequest();
            }

            GoodsController goodsController = new GoodsController(db);

            Goods good = goodsController.Put(orders.GoodsId, orders.Quantity);

            if (good == null)
            {
                return BadRequest();
            }

            db.Update(good);

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

