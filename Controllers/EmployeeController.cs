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


            /*** COMPANY START ***/

            // Get Entity objects from the list of Ids
            /*
                take note we are using 'where' clause here
            */
            var companys = _context.Company.Where(
                d => companyIds.Contains(d.Id)
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

            // get template objects
            var companyCalendars = _context.Calendar.Where(
                d => companyCalendarIds.Contains(d.Id)
            ).ToList();

            var companyPayrolls = _context.Payroll.Where(
                d => companyPayrollIds.Contains(d.Id)
            ).ToList();

            var companyWorkStatusTemplates = _context.WorkStatusTemplate.Where(
                d => companyWorkStatusTemplateIds.Contains(d.Id)
            ).ToList();

            /*** COMPANY END ***/


            /***  DEPARTMENT START ***/
            
            // get department Id's from the Entity Members
            var departmentIds = entityMembers.Select(
                d => d.DepartmentId
            ).ToList();

            // get the department objects
            var departments = _context.Department.Where(
                d => departmentIds.Contains(d.Id)
            ).ToList();

            // get the department Template IDs
            var departmentCalendarIds = departments.Select(
                d => d.CalendarId
            ).ToList();

            var departmentPayrollIds = departments.Select(
                d => d.PayrollId
            ).ToList();

            var departmentWorkStatusTemplateIds = departments.Select(
                d => d.WorkStatusTemplateId
            ).ToList();

            // get Department Template objects
            var departmentCalendars = _context.Calendar.Where(
                d => departmentCalendarIds.Contains(d.Id)
            ).ToList();

            var departmentPayrolls = _context.Payroll.Where(
                d => departmentPayrollIds.Contains(d.Id)
            ).ToList();

            var departmentWorkStatusTemplates = _context.WorkStatusTemplate.Where(
                d => departmentWorkStatusTemplateIds.Contains(d.Id)
            ).ToList();


            /*** DEPARTMENT END ***/


            /*** TEAM START ***/

            // get team Ids based on the entity mebers
            var teamIds = entityMembers.Select(
                d => d.TeamId
            ).ToList();

            // get team objects
            var teams = _context.Team.Where(
                d => teamIds.Contains(d.Id)
            ).ToList();

            // get team entity ids
            var teamCalendarIds = teams.Select(
                d => d.CalendarId
            ).ToList();

            var teamPayrollIds = teams.Select(
                d => d.PayrollId
            ).ToList();

            var teamWorkStatusTemplateIds = teams.Select(
                d => d.WorkStatusTemplateId
            ).ToList();

            // get Team Template objects
            var teamCalendars = _context.Calendar.Where(
                d => teamCalendarIds.Contains(d.Id)
            ).ToList();

            var teamPayrolls = _context.Payroll.Where(
                d => teamPayrollIds.Contains(d.Id)
            ).ToList();

            var teamWorkStatusTemplates = _context.WorkStatusTemplate.Where(
                d => teamWorkStatusTemplateIds.Contains(d.Id)
            ).ToList();


            /*** TEAM END ***/

            /*** SUBTEAM START ***/

            // get subteam Ids based on the entity mebers
            var subTeamIds = entityMembers.Select(
                d => d.SubTeamId
            ).ToList();
            
            // get sub team objects
            var subTeams = _context.SubTeam.Where(
                d => subTeamIds.Contains(d.Id)
            ).ToList();

            // get a template to be inherit based on the sub team and team hierarachy


            // Retrieve sub team calender START!

            long? subTeamCalendarId = 0;

            foreach (var subTeam in subTeams.OrderByDescending(s => s.Id)) {

                if (subTeamCalendarId == 0) {
                    // save the calender Id of the subteam
                    subTeamCalendarId = subTeam.CalendarId;

                    if (subTeamCalendarId == null) {
                        subTeamCalendarId = 0;
                    }

                    // the sub team calendar id is zero
                    if (subTeamCalendarId == 0) {

                        // check if this subteam has a parent Sub team
                        var lGetParent = true;
                        var iCurrSubTeamId = subTeam.Id;

                        while (lGetParent) {

                            var parentSubTeam = _context.SubTeam.FirstOrDefault(d => d.SubTeamId == iCurrSubTeamId);

                            if (parentSubTeam != null) {
                                // it has a parent sub team

                                // does this Parent sub team has calender ID? then use it and exit the loop
                                if (parentSubTeam.CalendarId != null && parentSubTeam.CalendarId != 0) {
                                    subTeamCalendarId = parentSubTeam.CalendarId;
                                    lGetParent = false;
                                }
                                else {
                                    // the parent sub team does not have calender ID, 
                                    
                                    // rerun loop to check if this parent subteam has another parent sub team as wll
                                    iCurrSubTeamId = parentSubTeam.Id;
                                }
                            }
                            else {
                                // it does not have any parent
                                lGetParent = false;
                            }
                        }
                    }
                }
                else {
                    break;
                }
      
            }

            if (subTeamCalendarId == 0) {
                // get the last team's calendar ID
                foreach (var subTeam in subTeams.OrderByDescending(s => s.Id)) {

                    if (subTeamCalendarId == 0) {

                        var parentTeam = _context.Team.FirstOrDefault(d => d.Id == subTeam.TeamId);

                        if (parentTeam != null) {
                            subTeamCalendarId = parentTeam.CalendarId;

                            if (subTeamCalendarId == null) {
                                subTeamCalendarId = 0;
                            }
                        }

                    }
                    else {
                        break;
                    }
        
                }
            }

            // get SubTeam Template objects
            var subTeamCalendars = _context.Calendar.Where(
                d => d.Id == subTeamCalendarId
            ).ToList();

            // Retrieve sub team calender END!


            // Retrieve sub team Payroll START!
            long? subTeamPayrollId = 0;

            foreach (var subTeam in subTeams.OrderByDescending(s => s.Id)) {

                if (subTeamPayrollId == 0) {
                    // save the payroll Id of the subteam
                    subTeamPayrollId = subTeam.PayrollId;

                    if (subTeamPayrollId == null) {
                        subTeamPayrollId = 0;
                    }

                    // the sub team Payroll id is zero
                    if (subTeamPayrollId == 0) {

                        // check if this subteam has a parent Sub team
                        var lGetParent = true;
                        var iCurrSubTeamId = subTeam.Id;

                        while (lGetParent) {

                            var parentSubTeam = _context.SubTeam.FirstOrDefault(d => d.SubTeamId == iCurrSubTeamId);

                            if (parentSubTeam != null) {
                                // it has a parent sub team

                                // does this Parent sub team has payroll ID? then use it and exit the loop
                                if (parentSubTeam.PayrollId != null && parentSubTeam.PayrollId != 0) {
                                    subTeamPayrollId = parentSubTeam.PayrollId;
                                    lGetParent = false;
                                }
                                else {
                                    // the parent sub team does not have payroll ID, 
                                    
                                    // rerun loop to check if this parent subteam has another parent sub team as wll
                                    iCurrSubTeamId = parentSubTeam.Id;
                                }
                            }
                            else {
                                // it does not have any parent
                                lGetParent = false;
                            }
                        }
                    }
                }
                else {
                    break;
                }
      
            }

            if (subTeamPayrollId == 0) {
                // get the last team's Payroll ID
                foreach (var subTeam in subTeams.OrderByDescending(s => s.Id)) {

                    if (subTeamPayrollId == 0) {

                        var parentTeam = _context.Team.FirstOrDefault(d => d.Id == subTeam.TeamId);

                        if (parentTeam != null) {
                            subTeamPayrollId = parentTeam.PayrollId;

                            if (subTeamPayrollId == null) {
                                subTeamPayrollId = 0;
                            }
                        }

                    }
                    else {
                        break;
                    }
        
                }
            }

            // get SubTeam Template objects
            var subTeamPayrolls = _context.Payroll.Where(
                d => d.Id == subTeamPayrollId
            ).ToList();

            // Retrieve sub team Payroll END!

            // Retrieve sub team Workstatus Template START!

            long? subTeamWorkStatusTemplateId = 0;

            foreach (var subTeam in subTeams.OrderByDescending(s => s.Id)) {

                if (subTeamWorkStatusTemplateId == 0) {
                    // save the workstatustemplate Id of the subteam
                    subTeamWorkStatusTemplateId = subTeam.WorkStatusTemplateId;

                    if (subTeamWorkStatusTemplateId == null) {
                        subTeamWorkStatusTemplateId = 0;
                    }

                    // the sub team WorkStatusTemplate id is zero
                    if (subTeamWorkStatusTemplateId == 0) {

                        // check if this subteam has a parent Sub team
                        var lGetParent = true;
                        var iCurrSubTeamId = subTeam.Id;

                        while (lGetParent) {

                            var parentSubTeam = _context.SubTeam.FirstOrDefault(d => d.SubTeamId == iCurrSubTeamId);

                            if (parentSubTeam != null) {
                                // it has a parent sub team

                                // does this Parent sub team has workstatustemplate ID? then use it and exit the loop
                                if (parentSubTeam.WorkStatusTemplateId != null && parentSubTeam.WorkStatusTemplateId != 0) {
                                    subTeamWorkStatusTemplateId = parentSubTeam.WorkStatusTemplateId;
                                    lGetParent = false;
                                }
                                else {
                                    // the parent sub team does not have workstatustemplate ID, 
                                    
                                    // rerun loop to check if this parent subteam has another parent sub team as wll
                                    iCurrSubTeamId = parentSubTeam.Id;
                                }
                            }
                            else {
                                // it does not have any parent
                                lGetParent = false;
                            }
                        }
                    }
                }
                else {
                    break;
                }
      
            }

            if (subTeamWorkStatusTemplateId == 0) {
                // get the last team's WorkStatusTemplate ID
                foreach (var subTeam in subTeams.OrderByDescending(s => s.Id)) {

                    if (subTeamWorkStatusTemplateId == 0) {

                        var parentTeam = _context.Team.FirstOrDefault(d => d.Id == subTeam.TeamId);

                        if (parentTeam != null) {
                            subTeamWorkStatusTemplateId = parentTeam.WorkStatusTemplateId;

                            if (subTeamWorkStatusTemplateId == null) {
                                subTeamWorkStatusTemplateId = 0;
                            }
                        }

                    }
                    else {
                        break;
                    }
        
                }
            }

            // get SubTeam Template objects
            var subTeamWorkStatusTemplates = _context.WorkStatusTemplate.Where(
                d => d.Id == subTeamWorkStatusTemplateId
            ).ToList();

            // Retrieve sub team Workstatus Template END!

        

            // the logic below are all wrong but comment it for now incase we need it later on...


            /*** SUB-SUBTEAM END ***/

            // // check if this sub-Team has also a child sub-team
            // var subSubTeamIds = subTeams.Select(
            //             d => d.SubTeamId
            //         ).ToList();

            // // if has sub-sub-team then loop for other
            // bool digSubTeams = subSubTeamIds.Any();

            // if (digSubTeams) {

            //     // store to a temporary variable 
            //     var subSubTeamIdsTmp = subSubTeamIds;

            //     // perform a loop
            //     while (digSubTeams == true) 
            //     {
            //         // get the subteam based onthe subTeamId
            //         var subSubTeam = _context.SubTeam.Where(
            //             d => subSubTeamIdsTmp.Contains(d.Id)
            //         ).ToList();

            //         // if no result then exit the loop
            //         if (!subSubTeam.Any()) {
            //             digSubTeams = false;
            //         }
            //         else {

            //             // refresh the temp variable 
            //             subSubTeamIdsTmp = subSubTeam.Select(
            //                 d => d.SubTeamId
            //             ).ToList();

            //             subSubTeamIds.AddRange(subSubTeamIdsTmp);  
            //         }

            //     }
            // }

            // // get sub team objects
            // var subSubTeams = _context.SubTeam.Where(
            //     d => subSubTeamIds.Contains(d.Id)
            // ).ToList();

            // // get sub-subteam template IDs
            // var subSubTeamCalendarIds = subSubTeams.Select(
            //     d => d.CalendarId
            // ).ToList();

            // var subSubTeamPayrollIds = subSubTeams.Select(
            //     d => d.PayrollId
            // ).ToList();

            // var subSubTeamWorkStatusTemplateIds = subSubTeams.Select(
            //     d => d.WorkStatusTemplateId
            // ).ToList();


            // // get SubTeam Template objects
            // var subSubTeamCalendars = _context.Calendar.Where(
            //     d => subSubTeamCalendarIds.Contains(d.Id)
            // ).ToList();

            // var subSubTeamPayrolls = _context.Payroll.Where(
            //     d => subSubTeamPayrollIds.Contains(d.Id)
            // ).ToList();

            // var subSubTeamWorkStatusTemplates = _context.WorkStatusTemplate.Where(
            //     d => subSubTeamWorkStatusTemplateIds.Contains(d.Id)
            // ).ToList();


            /*** SUB-SUBTEAM END ***/

            // a. loop all employees
            foreach (var emp in list) {
                // b. get employees entity members
                var entityMember = entityMembers.FirstOrDefault(d => d.EmployeeId == emp.Id);

                emp.Calendar = _context.Calendar.Find(emp.CalendarId);

                
                if (emp.Calendar == null) {

                    // // get from sub-sub team
                    // emp.Calendar = subSubTeamCalendars.LastOrDefault();

                    // get from sub team
                    if (emp.Calendar == null) {
                        emp.Calendar = subTeamCalendars.LastOrDefault();
                    }

                    // get  from team
                    if (emp.Calendar == null) {
                        emp.Calendar = teamCalendars.LastOrDefault();
                    }

                    // get from department
                    if (emp.Calendar == null) {
                        emp.Calendar = departmentCalendars.LastOrDefault();
                    }

                    // get from company
                    if (emp.Calendar == null) {
                        emp.Calendar = companyCalendars.LastOrDefault();
                    }
                }

                emp.Payroll = _context.Payroll.Find(emp.PayrollId);

                if (emp.Payroll == null) {

                    // // get from sub-sub team
                    // emp.Payroll = subSubTeamPayrolls.LastOrDefault();

                    // get from sub team
                    if (emp.Payroll == null) {
                        emp.Payroll = subTeamPayrolls.FirstOrDefault();
                    }

                    // get  from team
                    if (emp.Payroll == null) {
                        emp.Payroll = teamPayrolls.LastOrDefault();
                    }

                    // get from department
                    if (emp.Payroll == null) {
                        emp.Payroll = departmentPayrolls.LastOrDefault();
                    }

                    // get from company
                    if (emp.Payroll == null) {
                        emp.Payroll = companyPayrolls.LastOrDefault();
                    }
                }

                emp.WorkStatusTemplate = _context.WorkStatusTemplate.Find(emp.WorkStatusTemplateId);

                if (emp.WorkStatusTemplate == null) {

                    // // get from sub-sub team
                    // emp.WorkStatusTemplate = subSubTeamWorkStatusTemplates.LastOrDefault();

                    // get from sub team
                    if (emp.WorkStatusTemplate == null) {
                        emp.WorkStatusTemplate = subTeamWorkStatusTemplates.FirstOrDefault();
                    }

                    // get  from team
                    if (emp.WorkStatusTemplate == null) {
                        emp.WorkStatusTemplate = teamWorkStatusTemplates.LastOrDefault();
                    }

                    // get from department
                    if (emp.WorkStatusTemplate == null) {
                        emp.WorkStatusTemplate = departmentWorkStatusTemplates.LastOrDefault();
                    }

                    // get from company
                    if (emp.WorkStatusTemplate == null) {
                        emp.WorkStatusTemplate = companyWorkStatusTemplates.LastOrDefault();
                    }
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
