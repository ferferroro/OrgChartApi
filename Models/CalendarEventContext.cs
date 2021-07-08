using Microsoft.EntityFrameworkCore;
 
namespace OrgChartApi.Models
{
    public class CalendarEventContext : DbContext
    {
        public CalendarEventContext(DbContextOptions<CalendarEventContext> options)
            : base(options)
        {
        }
 
        public DbSet<CalendarEvent> CalendarEvent { get; set; }
    }
}