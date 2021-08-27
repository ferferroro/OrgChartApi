using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrgChartApi.Controllers.Base;
using OrgChartApi.Models;
using OrgChartApi.Models.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeamController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public TeamController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam(TeamRequest team)
        {
            
            IQueryable<Team> query = _context.Set<Team>();

            FilterEntityRequest(ref query, team);

            return await query
                .Include(p => p.SubTeam)
                .Include(p => p.Calendar)
                .Include(p => p.Payroll)
                .Include(p => p.WorkStatusTemplate)
                .Include(p => p.Employee)
                .Include("EntityMembers.Employee")
                .ToListAsync(); 
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(long id)
        {
            var team = await _context.Team.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(long id, TeamRequest team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            // _context.Entry(team).State = EntityState.Modified;
            _context.Entry(team).Property(p => p.Name).IsModified = team.Name != null;
            _context.Entry(team).Property(p => p.CalendarId).IsModified = team.CalendarId != null;
            _context.Entry(team).Property(p => p.PayrollId).IsModified = team.PayrollId != null;
            _context.Entry(team).Property(p => p.WorkStatusTemplateId).IsModified = team.WorkStatusTemplateId != null;
            _context.Entry(team).Property(p => p.EmployeeId).IsModified = team.EmployeeId != null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Team
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(TeamRequest team)
        {
            _context.Team.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(long id)
        {
            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Team.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(long id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
