
using System;
using System.ComponentModel.DataAnnotations;

namespace EStore.Models
{
    public class User
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool AccountStatus { get; set; }
    }
}
