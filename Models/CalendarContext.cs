using Microsoft.EntityFrameworkCore;
 
namespace OrgChartApi.Models
{
    public class CalendarContext : DbContext
    {
        public CalendarContext(DbContextOptions<CalendarContext> options)
            : base(options)
        {
        }
 
        public DbSet<Calendar> Calendar { get; set; }
    }
}