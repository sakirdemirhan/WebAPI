using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }
    }
}
