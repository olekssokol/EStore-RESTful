using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EStore.Models
{
    public class Order
    {
        public Int64 Id { get; set; }
        public int OrderNumber { get; set; }
        [ForeignKey("User")]
        public Int64 UserId { get; set; }
        public virtual User User { get; set; } // готовий зовнішній ключ, його потрібно добавити до інших таблиць
    }
}
