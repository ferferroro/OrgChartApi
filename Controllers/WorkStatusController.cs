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
    public class WorkStatusController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public WorkStatusController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/WorkStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkStatus>>> GetWorkStatus(WorkStatusRequest workStatus)
        {
            IQueryable<WorkStatus> query = _context.Set<WorkStatus>();

            FilterEntityRequest(ref query, workStatus);

            return await query.ToListAsync(); 
        }

        // GET: api/WorkStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkStatus>> GetWorkStatus(long id)
        {
            var workStatus = await _context.WorkStatus.FindAsync(id);

            if (workStatus == null)
            {
                return NotFound();
            }

            return workStatus;
        }

        // PUT: api/WorkStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkStatus(long id, WorkStatus workStatus)
        {
            if (id != workStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(workStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkStatusExists(id))
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

        // POST: api/WorkStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkStatus>> PostWorkStatus(WorkStatusRequest workStatus)
        {
            _context.WorkStatus.Add(workStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkStatus", new { id = workStatus.Id }, workStatus);
        }

        // DELETE: api/WorkStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkStatus(long id)
        {
            var workStatus = await _context.WorkStatus.FindAsync(id);
            if (workStatus == null)
            {
                return NotFound();
            }

            _context.WorkStatus.Remove(workStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkStatusExists(long id)
        {
            return _context.WorkStatus.Any(e => e.Id == id);
        }
    }
}
