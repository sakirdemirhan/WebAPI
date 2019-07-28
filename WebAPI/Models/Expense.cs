using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
