using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime ComingDate { get; set; }
        public decimal Price { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
