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
    public class WorkStatusTemplateController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public WorkStatusTemplateController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/WorkStatusTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkStatusTemplate>>> GetWorkStatusTemplate(WorkStatusTemplateRequest workStatusTemplate)
        {
            IQueryable<WorkStatusTemplate> query = _context.Set<WorkStatusTemplate>();

            FilterEntityRequest(ref query, workStatusTemplate);

            return await query.ToListAsync(); 
        }

        // GET: api/WorkStatusTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkStatusTemplate>> GetWorkStatusTemplate(long id)
        {
            var workStatusTemplate = await _context.WorkStatusTemplate.FindAsync(id);

            if (workStatusTemplate == null)
            {
                return NotFound();
            }

            return workStatusTemplate;
        }

        // PUT: api/WorkStatusTemplate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkStatusTemplate(long id, WorkStatusTemplate workStatusTemplate)
        {
            if (id != workStatusTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(workStatusTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkStatusTemplateExists(id))
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

        // POST: api/WorkStatusTemplate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkStatusTemplate>> PostWorkStatusTemplate(WorkStatusTemplateRequest workStatusTemplate)
        {
            _context.WorkStatusTemplate.Add(workStatusTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkStatusTemplate", new { id = workStatusTemplate.Id }, workStatusTemplate);
        }

        // DELETE: api/WorkStatusTemplate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkStatusTemplate(long id)
        {
            var workStatusTemplate = await _context.WorkStatusTemplate.FindAsync(id);
            if (workStatusTemplate == null)
            {
                return NotFound();
            }

            _context.WorkStatusTemplate.Remove(workStatusTemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkStatusTemplateExists(long id)
        {
            return _context.WorkStatusTemplate.Any(e => e.Id == id);
        }
    }
}
