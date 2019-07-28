using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ValuesController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET api/values/GetIncomesByMonths
        [HttpGet("GetIncomesByMonths")]
        public ActionResult<IEnumerable<decimal>> GetIncomesByMonths()
        {
            decimal total = 0;
            var completedWorks = _context.Items.Where(x => x.IsCompleted == true && x.IsDeleted == false).ToList();
            var numbers = new List<decimal>();

            for (int i = 1; i < 13; i++)
            {
                foreach (var item in completedWorks.Where(x => x.ComingDate.Year == DateTime.Now.Year && x.ComingDate.Month == i))
                {
                    total += item.Count * item.Price;
                }
                numbers.Add(total);
                total = 0;
            }
            return numbers;
        }

        // GET api/values/GetCompletedItemsByMonths
        [HttpGet("GetCompletedItemsByMonths")]
        public ActionResult<IEnumerable<int>> GetCompletedItemsByMonths()
        {
            int total = 0;
            var completedWorks = _context.Items.Where(x => x.IsCompleted == true && x.IsDeleted == false).ToList();
            var counts = new List<int>();

            for (int i = 1; i < 13; i++)
            {
                foreach (var item in completedWorks.Where(x => x.ComingDate.Year == DateTime.Now.Year && x.ComingDate.Month == i))
                {
                    total += item.Count;
                }
                counts.Add(total);
                total = 0;
            }
            return counts;
        }

        // GET api/values/GetAllCompletedItems
        [HttpGet("GetAllCompletedItems")]
        public ActionResult<int> GetAllCompletedItems()
        {
            return _context.Items.Where(x => x.IsCompleted == true && x.IsDeleted == false).ToList().Count;
        }

        // GET api/values/GetAllEmployees
        [HttpGet("GetAllEmployees")]
        public ActionResult<int> GetAllEmployees()
        {
            return _context.Employees.Where(x => x.IsDeleted == false).ToList().Count;
        }

        // GET api/values/GetAllIncomes
        [HttpGet("GetAllIncomes")]
        public ActionResult<decimal> GetAllIncomes()
        {
            decimal total = 0;
            var completedWorks = _context.Items.Where(x => x.IsCompleted == true && x.IsDeleted == false).ToList();
            foreach (var item in completedWorks)
            {
                total += item.Count * item.Price;
            }
            return total;
        }

        //pie chart 1
        // GET api/values/GetIncomeAndExpenseOfThisMonth
        [HttpGet("GetIncomeAndExpenseOfThisMonth")]
        public ActionResult<IEnumerable<decimal>> GetIncomeAndExpenseOfThisMonth()
        {
            decimal totalIncomes = 0;
            decimal totalExpense = 0;
            var completedWorks = _context.Items.Where(x => x.IsCompleted == true && x.IsDeleted == false).ToList();
            var expenses = _context.Expense.Where(x => x.IsDeleted == false).ToList();
            var numbers = new List<decimal>();
            foreach (var item in completedWorks.Where(x=>x.ComingDate.Year == DateTime.Now.Year && x.ComingDate.Month == DateTime.Now.Month))
            {
                totalIncomes += item.Count * item.Price;
            }
            numbers.Add(totalIncomes);
            foreach (var item in expenses.Where(x => x.ExpenseDate.Year == DateTime.Now.Year && x.ExpenseDate.Month == DateTime.Now.Month))
            {
                totalExpense += item.Amount;
            }
            numbers.Add(totalExpense);
            return numbers;
        }

        //pie chart 2
        // GET api/values/GetItemsOfThisMonth
        [HttpGet("GetItemsOfThisMonth")]
        public ActionResult<IEnumerable<int>> GetItemsOfThisMonth()
        {
            var numbers = new List<int>();

            int completedWorks = _context.Items.Where(x => x.IsCompleted == true 
            && x.IsDeleted == false
            && x.ComingDate.Year == DateTime.Now.Year && x.ComingDate.Month == DateTime.Now.Month
            ).ToList().Count;

            int onProcessWorks = _context.Items.Where(x => x.IsCompleted == false 
            && x.IsDeleted == false
            && x.ComingDate.Year == DateTime.Now.Year && x.ComingDate.Month == DateTime.Now.Month
            ).ToList().Count;

            numbers.Add(completedWorks);
            numbers.Add(onProcessWorks);
            return numbers;
        }

        #region old
        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        #endregion old
    }
}
