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
    public class EmployeeControllerBackUp : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public EmployeeControllerBackUp(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee(EmployeeRequest employee)
        {

            IQueryable<Employee> query = _context.Set<Employee>();

            FilterEntityRequest(ref query, employee);

            // experiment plan start

            /*
                1. fetch all employee
                2. fetch all associated entity
                3. fetch all templates of employee
                4. fetch all templates of entities
                5. process each template inheritance
                    a. loop all employees
                    b. get employees entity members
                    c. check if employee has templates
                    d. if null then check the next available entity based on heirarchy
            */

            // experiment plan end

            // actual start


             // get all employees
            var list = query.ToList();

            // get all employee ids and store to another array variable
            var empIds = list.Select(
                d => d.Id
            ).ToList();

            // using the employee ids from above, get all enity members
            var entityMembers = _context.EntityMembers.Where(
                d => empIds.Contains(d.EmployeeId.GetValueOrDefault())
            ).ToList();

            // get company Ids based on the entity mebers
            /**
            * Sql equivalent query:
            * Select CompanyId from EntityMembers
            */
            var companyIds = entityMembers.Select(
                d => d.CompanyId
            ).ToList();

            // get department Ids based on the entity mebers
            var departmentIds = entityMembers.Select(
                d => d.DepartmentId
            ).ToList();

            // get team Ids based on the entity mebers
            var teamIds = entityMembers.Select(
                d => d.TeamId
            ).ToList();

            // get subteam Ids based on the entity mebers
            var subTeamIds = entityMembers.Select(
                d => d.SubTeamId
            ).ToList();

            // Get Entity objects from the list of Ids
            /*
                take note we are using 'where' clause here
            */
            var companys = _context.Company.Where(
                d => companyIds.Contains(d.Id)
            ).ToList();

            var departments = _context.Department.Where(
                d => departmentIds.Contains(d.Id)
            ).ToList();

            var teams = _context.Team.Where(
                d => teamIds.Contains(d.Id)
            ).ToList();

            var subTeams = _context.SubTeam.Where(
                d => subTeamIds.Contains(d.Id)
            ).ToList();

            // Get Templates Ids of entities
            var companyCalendarIds = companys.Select(
                d => d.CalendarId
            ).ToList();

            var companyPayrollIds = companys.Select(
                d => d.PayrollId
            ).ToList();

            var companyWorkStatusTemplateIds = companys.Select(
                d => d.WorkStatusTemplateId
            ).ToList();

            var departmentCalendarIds = departments.Select(
                d => d.CalendarId
            ).ToList();

            var departmentPayrollIds = departments.Select(
                d => d.PayrollId
            ).ToList();

            var departmentWorkStatusTemplateIds = departments.Select(
                d => d.WorkStatusTemplateId
            ).ToList();

            var teamCalendarIds = teams.Select(
                d => d.CalendarId
            ).ToList();

            var teamPayrollIds = teams.Select(
                d => d.PayrollId
            ).ToList();

            var teamWorkStatusTemplateIds = teams.Select(
                d => d.WorkStatusTemplateId
            ).ToList();

            var subTeamCalendarIds = subTeams.Select(
                d => d.CalendarId
            ).ToList();

            var subTeamPayrollIds = subTeams.Select(
                d => d.PayrollId
            ).ToList();

            var subTeamWorkStatusTemplateIds = subTeams.Select(
                d => d.WorkStatusTemplateId
            ).ToList();


            // get template ids
            var calendarIds = list.Select(
                d => d.CalendarId
            ).ToList();

            var payrollIds = list.Select(
                d => d.PayrollId
            ).ToList();

            var workStatusTemplateIds = list.Select(
                d => d.WorkStatusTemplateId
            ).ToList();

            
                //SEPARATE TEMPLATE FETCH FOR EVERY INTITY


            // combine or consolidate Template Ids
            calendarIds.AddRange(companyCalendarIds);
            calendarIds.AddRange(departmentCalendarIds);
            calendarIds.AddRange(teamCalendarIds);
            calendarIds.AddRange(subTeamCalendarIds);   

            payrollIds.AddRange(companyPayrollIds);
            payrollIds.AddRange(departmentPayrollIds);
            payrollIds.AddRange(teamPayrollIds);
            payrollIds.AddRange(subTeamPayrollIds);      

            workStatusTemplateIds.AddRange(companyWorkStatusTemplateIds);
            workStatusTemplateIds.AddRange(departmentWorkStatusTemplateIds);
            workStatusTemplateIds.AddRange(teamWorkStatusTemplateIds);
            workStatusTemplateIds.AddRange(subTeamWorkStatusTemplateIds);   

            // get template objects
            var calendars = _context.Calendar.Where(
                d => calendarIds.Contains(d.Id)
            ).ToList();

            var payrolls = _context.Payroll.Where(
                d => payrollIds.Contains(d.Id)
            ).ToList();

            var workStatusTemplates = _context.WorkStatusTemplate.Where(
                d => workStatusTemplateIds.Contains(d.Id)
            ).ToList();

            

            /**
            copy of comment from above...
            5. process each template inheritance
                    a. loop all employees
                    b. get employees entity members
                    c. check if employee has templates
                    d. if null then check the next available entity based on heirarchy
            */

            // a. loop all employees
            foreach (var emp in list) {
                // b. get employees entity members
                var entityMember = entityMembers.FirstOrDefault(d => d.EmployeeId == emp.Id);

                
                if (emp.CalendarId == null || emp.CalendarId == 0) {
                    // todo create your own way of inheriting whatever the next avaialble template
                    emp.Calendar = calendars.LastOrDefault();

                    // get from sub team

                    // get  from team

                    // get from department

                    // get from company


                    // emp.Calendar = _context.Calendar.Find(1);
                }

                if (emp.PayrollId == null || emp.PayrollId == 0) {
                    emp.Payroll = payrolls.FirstOrDefault();
                }

                if (emp.WorkStatusTemplateId == null || emp.WorkStatusTemplateId == 0) {
                    emp.WorkStatusTemplate = workStatusTemplates.FirstOrDefault();
                }

            } 

            return list;


            // actual end

            // return await query
            //     .Include(p => p.Calendar)
            //     .Include(p => p.Payroll)
            //     .Include(p => p.WorkStatusTemplate)
            //     .Include("EntityMembers.Employee")
            //     .Include("EntityMembers.Company")
            //     .Include("EntityMembers.Department")
            //     .Include("EntityMembers.Team")
            //     .Include("EntityMembers.SubTeam")
            //     .ToListAsync();        
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
