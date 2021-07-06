using Microsoft.EntityFrameworkCore;
 
namespace OrgChartApi.Models
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext(DbContextOptions<DepartmentContext> options)
            : base(options)
        {
        }
 
        public DbSet<Department> Department { get; set; }
 
        public DbSet<Company> Company { get; set; }
    }
}