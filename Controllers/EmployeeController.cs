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
using OrgChartApi.Controllers.Base;
using OrgChartApi.Models.DTOs.Requests;

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public EmployeeController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(EmployeeRequest employee)
        {

            IQueryable<Employee> query = _context.Set<Employee>();

            FilterEntityRequest(ref query, employee);

            return await query
                .Include(p => p.Calendar)
                .Include(p => p.Payroll)
                .Include(p => p.WorkStatusTemplate)
                .Include(p => p.EntityMembers)
                .ToListAsync();        
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, EmployeeRequest employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            // below is the original code
            // _context.Entry(employee).State = EntityState.Modified;
                        
            // _context.Employee.Attach(employee);

            var local = _context.Set<Employee>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(id));

            // check if local is not null 
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            

            // only if if the value is not null, the field will change.
            _context.Entry(employee).Property(p => p.FirstName).IsModified = employee.FirstName != null;
            _context.Entry(employee).Property(p => p.LastName).IsModified = employee.LastName != null;
            // _context.Entry(employee).Property(p => p.Username).IsModified = employee.Username != null;
            // _context.Entry(employee).Property(p => p.Password).IsModified = false;


            _context.Entry(employee).Property(p => p.CalendarId).IsModified = employee.CalendarId != null;
            _context.Entry(employee).Property(p => p.PayrollId).IsModified = employee.PayrollId != null;
            _context.Entry(employee).Property(p => p.WorkStatusTemplateId).IsModified = employee.WorkStatusTemplateId != null;
            
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

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeRequest employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
