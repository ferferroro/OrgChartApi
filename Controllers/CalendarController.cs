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
    public class CalendarController : EntityBaseController
    {
        private readonly OrgChartContext _context;

        public CalendarController(OrgChartContext context)
        {
            _context = context;
        }

        // GET: api/Calendar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendar(CalendarRequest calendar)
        {
            IQueryable<Calendar> query = _context.Set<Calendar>();

            FilterEntityRequest(ref query, calendar);

            return await query.ToListAsync(); 
        }

        // GET: api/Calendar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(long id)
        {
            var calendar = await _context.Calendar.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            return calendar;
        }

        // PUT: api/Calendar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendar(long id, CalendarRequest calendar)
        {
            if (id != calendar.Id)
            {
                return BadRequest();
            }

            _context.Entry(calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
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

        // POST: api/Calendar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar(CalendarRequest calendar)
        {
            _context.Calendar.Add(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendar", new { id = calendar.Id }, calendar);
        }

        // DELETE: api/Calendar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(long id)
        {
            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            _context.Calendar.Remove(calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarExists(long id)
        {
            return _context.Calendar.Any(e => e.Id == id);
        }
    }
}
