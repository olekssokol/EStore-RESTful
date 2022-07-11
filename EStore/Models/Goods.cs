using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class Goods
    {
        [Display(Name = "Id")]
        public Int64 GoodsId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int PriceForOne { get; set; }
    }
}
