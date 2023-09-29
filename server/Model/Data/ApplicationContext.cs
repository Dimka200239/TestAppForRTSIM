using Microsoft.EntityFrameworkCore;
using server.Model;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace server.Model.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; private set; }
        public DbSet<Organization> Organizations { get; private set; }
        public DbSet<EmployeeOrganizationMap> EmployeeOrganizationMaps { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestAppDB;Trusted_Connection=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeOrganizationMap>()
                .HasKey(eom => new { eom.loginEmp, eom.orgId });
        }
    }
}
