using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Overtime
    {
        public int OvertimeId { get; set; }
        public int EmployeeId { get; set; }
        public int Hour { get; set; }
        public DateTime OvertimeDay { get; set; }
        public bool IsDeleted { get; set; }
    }
}
