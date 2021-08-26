using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
// using System.Data.Entity.Core.ModelConfiguration;
 
namespace OrgChartApi.Models
{
    public class OrgChartContext : IdentityDbContext
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
        public DbSet<Payroll> Payroll { get; set; }      

        public DbSet<WorkStatusTemplate> WorkStatusTemplate { get; set; } 
        public DbSet<WorkStatus> WorkStatus { get; set; } 
        public DbSet<WorkStatuses> WorkStatuses { get; set; } 
        public DbSet<Team> Team { get; set; } 
        public DbSet<SubTeam> SubTeam { get; set; } 
        public DbSet<EntityMembers> EntityMembers { get; set; } 

    }
}