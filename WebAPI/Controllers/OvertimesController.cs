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
    public class OvertimesController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public OvertimesController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Overtimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Overtime>>> GetOvertime()
        {
            return await _context.Overtime.Where(x => x.IsDeleted == false).OrderByDescending(x => x.OvertimeId).ToListAsync();
        }

        // GET: api/GetOvertimeListByEmployeeId/5
        [HttpGet("GetOvertimeListByEmployeeId/{id}")]
        public async Task<ActionResult<IEnumerable<Overtime>>> GetOvertimeListByEmployeeId(int id)
        {
            return await _context.Overtime.Where(x => x.EmployeeId == id && x.IsDeleted == false).ToListAsync();
        }

        // GET: api/GetOvertimeListDateBetween/5/5/2019
        [HttpGet("GetOvertimeListDateBetween/{id}/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<Overtime>>> GetOvertimeListDateBetween(int id, int month, int year)
        {
            return await _context.Overtime.Where(
                x => x.EmployeeId == id
                && x.OvertimeDay.Year == year
                && x.OvertimeDay.Month == month
                && x.IsDeleted == false
                ).ToListAsync();
        }

        // GET: api/Overtimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Overtime>> GetOvertime(int id)
        {
            var overtime = await _context.Overtime.FirstAsync(x => x.IsDeleted == false && x.OvertimeId == id);

            if (overtime == null)
            {
                return NotFound();
            }

            return overtime;
        }

        // PUT: api/Overtimes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOvertime(int id, Overtime overtime)
        {
            if (id != overtime.OvertimeId)
            {
                return BadRequest();
            }

            _context.Entry(overtime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OvertimeExists(id))
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

        // POST: api/Overtimes
        [HttpPost]
        public async Task<ActionResult<Overtime>> PostOvertime(Overtime overtime)
        {
            _context.Overtime.Add(overtime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOvertime", new { id = overtime.OvertimeId }, overtime);
        }

        // DELETE: api/Overtimes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Overtime>> DeleteOvertime(int id)
        {
            try
            {
                var overtime = _context.Overtime.First(x => x.OvertimeId == id);
                overtime.IsDeleted = true;
                _context.Entry(overtime).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OvertimeExists(id))
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

        [HttpPost("DeleteBulk")]
        public async Task<ActionResult<List<Overtime>>> DeleteBulk(List<Overtime> overtimes)
        {
            if (overtimes == null || overtimes.Count <= 0)
            {
                return NotFound();
            }

            foreach (var emp in overtimes)
            {
                var obj = _context.Overtime.First(x => x.OvertimeId == emp.OvertimeId);
                obj.IsDeleted = true;
            }

            await _context.SaveChangesAsync();

            return overtimes;
        }

        private bool OvertimeExists(int id)
        {
            return _context.Overtime.Any(e => e.OvertimeId == id);
        }
    }
}
