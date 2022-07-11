using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class Orders
    {
        public Int64 Id { get; set; }
        [ForeignKey("Order")]
        public Int64 OrderId { get; set; }
        [ForeignKey("Goods")]
        public Int64 GoodsId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
