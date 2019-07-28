using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ExpensesController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpense()
        {
            return await _context.Expense.Where(x => x.IsDeleted == false).OrderByDescending(x => x.ExpenseId).ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expense.FirstAsync(x => x.IsDeleted == false && x.ExpenseId == id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _context.Expense.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            try
            {
                var expense = _context.Expense.First(x => x.ExpenseId == id);
                expense.IsDeleted = true;
                _context.Entry(expense).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            //var expense = await _context.Expense.FindAsync(id);
            //if (expense == null)
            //{
            //    return NotFound();
            //}

            //_context.Expense.Remove(expense);
            //await _context.SaveChangesAsync();

            //return expense;
        }

        // DELETE: api/Expenses/DeleteBulk
        [HttpPost("DeleteBulk")]
        public async Task<ActionResult<List<Expense>>> DeleteBulk(List<Expense> expenses)
        {
            if (expenses == null || expenses.Count <= 0)
            {
                return NotFound();
            }

            foreach (var emp in expenses)
            {
                var obj = _context.Expense.First(x => x.ExpenseId == emp.ExpenseId);
                obj.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            return expenses;
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.ExpenseId == id);
        }
    }
}
