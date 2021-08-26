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
    public class DepartmentController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public DepartmentController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment(DepartmentRequest department)
        {            
            IQueryable<Department> query = _context.Set<Department>();

            FilterEntityRequest(ref query, department);

            return await query
                .Include(p => p.Calendar)
                .Include(p => p.Payroll)
                .Include(p => p.WorkStatusTemplate)
                .Include(p => p.Employee)
                .Include(p => p.EntityMembers)
                .ToListAsync(); 
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(long id)
        {
            var department = await _context.Department.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(long id, DepartmentRequest department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            // _context.Entry(department).State = EntityState.Modified;
            _context.Entry(department).Property(p => p.Name).IsModified = department.Name != null;
            _context.Entry(department).Property(p => p.CalendarId).IsModified = department.CalendarId != null;
            _context.Entry(department).Property(p => p.PayrollId).IsModified = department.PayrollId != null;
            _context.Entry(department).Property(p => p.WorkStatusTemplateId).IsModified = department.WorkStatusTemplateId != null;
            _context.Entry(department).Property(p => p.EmployeeId).IsModified = department.EmployeeId != null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(DepartmentRequest department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(long id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(long id)
        {
            return _context.Department.Any(e => e.Id == id);
        }

    }
}
