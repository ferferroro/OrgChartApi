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

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        private readonly OrgChartContext _context;

        public EmployeeController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(Employee employee)
        {

            IQueryable<Employee> query = _context.Set<Employee>();

            if (employee.FindById != 0) {
                query = query.Where(t => 
                    t.Id == employee.FindById
                );
            }
            else {
                if (employee.FindByFirstName != null) {
                    query = query.Where(t => 
                        t.FirstName.Contains(employee.FindByFirstName) 
                    );
                }

                if (employee.FindByLastName != null) {
                    query = query.Where(t => 
                        t.LastName.Contains(employee.FindByLastName) 
                    );
                }

                // if (employee.FindByUsername != null) {
                //     query = query.Where(t => 
                //         t.Username.Contains(employee.FindByUsername) 
                //     );
                // }

            } 

            if (employee.SortField != null) {

                string sortOrder = employee.SortOrder;

                if (sortOrder == null) {
                    sortOrder = "asc";
                }


                if (sortOrder == "asc") {

                    if (employee.SortField == "Id") {
                        query = query.OrderBy(t => 
                            t.Id
                        );
                    }

                    if (employee.SortField == "Firstname") {
                        query = query.OrderBy(t => 
                            t.FirstName
                        );
                    }

                    if (employee.SortField == "LastName") {
                        query = query.OrderBy(t => 
                            t.LastName
                        );
                    }

                    // if (employee.SortField == "Username") {
                    //     query = query.OrderBy(t => 
                    //         t.Username
                    //     );
                    // }
                }

                if (sortOrder == "desc") {

                    if (employee.SortField == "Id") {
                        query = query.OrderByDescending(t => 
                            t.Id
                        );
                    }

                    if (employee.SortField == "Firstname") {
                        query = query.OrderByDescending(t => 
                            t.FirstName
                        );
                    }

                    if (employee.SortField == "LastName") {
                        query = query.OrderByDescending(t => 
                            t.LastName
                        );
                    }

                    // if (employee.SortField == "Username") {
                    //     query = query.OrderByDescending(t => 
                    //         t.Username
                    //     );
                    // }
                }
                
            }

            int pageSize = employee.FindByPageSize;
            if (pageSize == 0) {
                pageSize = 20;
            }

            int pageNumber = employee.FindByPageNumber;
            if (pageNumber == 0) {
                pageNumber = 1;
            }

            query = query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return await query.ToListAsync(); 

            // below is our original scafolded code           
            // return await _context.Employee.ToListAsync();            
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
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
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
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
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
