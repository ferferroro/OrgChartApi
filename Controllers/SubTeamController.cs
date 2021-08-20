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
    public class SubTeamController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public SubTeamController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/SubTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTeam>>> GetSubTeam(SubTeamRequest subTeam)
        {
            IQueryable<SubTeam> query = _context.Set<SubTeam>();

            FilterEntityRequest(ref query, subTeam);

            return await query
                .Include(p => p.Team)
                .Include(p => p.SubSubTeam)
                .ToListAsync(); 
        }

        // GET: api/SubTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubTeam>> GetSubTeam(long id)
        {
            var subTeam = await _context.SubTeam.FindAsync(id);

            if (subTeam == null)
            {
                return NotFound();
            }

            return subTeam;
        }

        // PUT: api/SubTeam/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubTeam(long id, SubTeamRequest subTeam)
        {
            if (id != subTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(subTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubTeamExists(id))
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

        // POST: api/SubTeam
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubTeam>> PostSubTeam(SubTeamRequest subTeam)
        {
            _context.SubTeam.Add(subTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubTeam", new { id = subTeam.Id }, subTeam);
        }

        // DELETE: api/SubTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTeam(long id)
        {
            var subTeam = await _context.SubTeam.FindAsync(id);
            if (subTeam == null)
            {
                return NotFound();
            }

            _context.SubTeam.Remove(subTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubTeamExists(long id)
        {
            return _context.SubTeam.Any(e => e.Id == id);
        }
    }
}