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
    public class EmployeesController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public EmployeesController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.Where(x=>x.IsDeleted == false).OrderByDescending(x=>x.EmployeeId).ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FirstAsync(x => x.IsDeleted == false && x.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employee = _context.Employees.First(x => x.EmployeeId == id);
                employee.IsDeleted = true;
                _context.Entry(employee).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // DELETE: api/Employees/DeleteBulk
        [HttpPost("DeleteBulk")]
        public async Task<ActionResult<List<Employee>>> DeleteBulk(List<Employee> employees)
        {
            if (employees == null || employees.Count <= 0)
            {
                return NotFound();
            }

            foreach (var emp in employees)
            {
                var obj = _context.Employees.First(x=>x.EmployeeId == emp.EmployeeId);
                obj.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            return employees;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        // GET: api/Employees/HasEmployee/1
        [HttpGet("HasEmployee/{id}")]
        public ActionResult<bool> HasEmployee(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
