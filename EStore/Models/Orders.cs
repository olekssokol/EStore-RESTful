using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GoodsId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
