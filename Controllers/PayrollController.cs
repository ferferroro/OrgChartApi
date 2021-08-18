using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrgChartApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OrgChartApi.Models.DTOs.Requests;
using OrgChartApi.Controllers.Base;

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PayrollController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public PayrollController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Payroll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payroll>>> GetPayroll(PayrollRequest payroll)
        {
            IQueryable<Payroll> query = _context.Set<Payroll>();

            FilterEntityRequest(ref query, payroll);

            return await query.ToListAsync(); 
        }

        // GET: api/Payroll/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payroll>> GetPayroll(long id)
        {
            var payroll = await _context.Payroll.FindAsync(id);

            if (payroll == null)
            {
                return NotFound();
            }

            return payroll;
        }

        // PUT: api/Payroll/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayroll(long id, PayrollRequest payroll)
        {
            if (id != payroll.Id)
            {
                return BadRequest();
            }

            _context.Entry(payroll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PayrollExists(id))
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

        // POST: api/Payroll
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payroll>> PostPayroll(PayrollRequest payroll)
        {
            _context.Payroll.Add(payroll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPayroll", new { id = payroll.Id }, payroll);
        }

        // DELETE: api/Payroll/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayroll(long id)
        {
            var payroll = await _context.Payroll.FindAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }

            _context.Payroll.Remove(payroll);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PayrollExists(long id)
        {
            return _context.Payroll.Any(e => e.Id == id);
        }
    }
}
