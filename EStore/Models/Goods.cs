using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class Goods
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int PriceForOne { get; set; }
    }
}
