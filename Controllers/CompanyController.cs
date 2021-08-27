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
    public class CompanyController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public CompanyController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany(CompanyRequest company)
        {
            IQueryable<Company> query = _context.Set<Company>();

            FilterEntityRequest(ref query, company);

            return await query
                .Include(p => p.Calendar)
                .Include(p => p.Payroll)
                .Include(p => p.WorkStatusTemplate)
                .Include(p => p.Employee)
                .Include("EntityMembers.Employee")
                .ToListAsync(); 

            // below is the original scafolded code:
            // return await _context.Company.ToListAsync();
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(long id)
        {
            var company = await _context.Company.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Company/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(long id, CompanyRequest company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            // _context.Entry(company).State = EntityState.Modified;
            _context.Entry(company).Property(p => p.Name).IsModified = company.Name != null;
            _context.Entry(company).Property(p => p.CalendarId).IsModified = company.CalendarId != null;
            _context.Entry(company).Property(p => p.PayrollId).IsModified = company.PayrollId != null;
            _context.Entry(company).Property(p => p.WorkStatusTemplateId).IsModified = company.WorkStatusTemplateId != null;
            _context.Entry(company).Property(p => p.EmployeeId).IsModified = company.EmployeeId != null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyRequest company)
        {
            _context.Company.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Company.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(long id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
    }
}
