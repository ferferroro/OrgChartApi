using Microsoft.EntityFrameworkCore;
 
namespace OrgChartApi.Models
{
    public class OrgChartContext : DbContext
    {
        public OrgChartContext(DbContextOptions<OrgChartContext> options)
            : base(options)
        {
        }
 
        public DbSet<Company> Company { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Calendar> Calendar { get; set; }
        public DbSet<CalendarEvent> CalendarEvent { get; set; }
    }
}