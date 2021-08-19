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
    public class WorkStatusesController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public WorkStatusesController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/WorkStatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkStatuses>>> GetWorkStatuses()
        {
            return await _context.WorkStatuses.ToListAsync();
        }

        // GET: api/WorkStatuses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkStatuses>> GetWorkStatuses(long id)
        {
            var workStatuses = await _context.WorkStatuses.FindAsync(id);

            if (workStatuses == null)
            {
                return NotFound();
            }

            return workStatuses;
        }

        // PUT: api/WorkStatuses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkStatuses(long id, WorkStatusesRequest workStatuses)
        {
            if (id != workStatuses.Id)
            {
                return BadRequest();
            }

            _context.Entry(workStatuses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkStatusesExists(id))
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

        // POST: api/WorkStatuses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkStatuses>> PostWorkStatuses(WorkStatusesRequest workStatuses)
        {
            _context.WorkStatuses.Add(workStatuses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkStatuses", new { id = workStatuses.Id }, workStatuses);
        }

        // DELETE: api/WorkStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkStatuses(long id)
        {
            var workStatuses = await _context.WorkStatuses.FindAsync(id);
            if (workStatuses == null)
            {
                return NotFound();
            }

            _context.WorkStatuses.Remove(workStatuses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkStatusesExists(long id)
        {
            return _context.WorkStatuses.Any(e => e.Id == id);
        }
    }
}
