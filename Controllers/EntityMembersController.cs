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

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EntityMembersController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public EntityMembersController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/EntityMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityMembers>>> GetEntityMembers(EntityMembersRequest entityMembers)
        {
            IQueryable<EntityMembers> query = _context.Set<EntityMembers>();

            FilterEntityRequest(ref query, entityMembers);

            return await query
                .Include(p => p.Employee)
                .Include(p => p.Company)
                .Include(p => p.Department)
                .Include(p => p.Team)
                .Include(p => p.SubTeam)
                .ToListAsync();   
        }

        // GET: api/EntityMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityMembers>> GetEntityMembers(long id)
        {
            var entityMembers = await _context.EntityMembers.FindAsync(id);

            if (entityMembers == null)
            {
                return NotFound();
            }

            return entityMembers;
        }

        // PUT: api/EntityMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntityMembers(long id, EntityMembersRequest entityMembers)
        {
            if (id != entityMembers.Id)
            {
                return BadRequest();
            }

            // _context.Entry(entityMembers).State = EntityState.Modified;
            _context.Entry(entityMembers).Property(p => p.EmployeeId).IsModified = entityMembers.EmployeeId != null;
            _context.Entry(entityMembers).Property(p => p.CompanyId).IsModified = entityMembers.CompanyId != null;
            _context.Entry(entityMembers).Property(p => p.DepartmentId).IsModified = entityMembers.DepartmentId != null;
            _context.Entry(entityMembers).Property(p => p.TeamId).IsModified = entityMembers.TeamId != null;
            _context.Entry(entityMembers).Property(p => p.SubTeamId).IsModified = entityMembers.SubTeamId != null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityMembersExists(id))
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

        // POST: api/EntityMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntityMembers>> PostEntityMembers(EntityMembersRequest entityMembers)
        {
            _context.EntityMembers.Add(entityMembers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntityMembers", new { id = entityMembers.Id }, entityMembers);
        }

        // DELETE: api/EntityMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityMembers(long id)
        {
            var entityMembers = await _context.EntityMembers.FindAsync(id);
            if (entityMembers == null)
            {
                return NotFound();
            }

            _context.EntityMembers.Remove(entityMembers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntityMembersExists(long id)
        {
            return _context.EntityMembers.Any(e => e.Id == id);
        }
    }
}
