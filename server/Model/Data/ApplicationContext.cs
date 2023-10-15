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
        public DbSet<Gender> Genders { get; private set; }
        public DbSet<Users> Userss { get; private set; }
        public DbSet<Comments> Commentss { get; private set; }

        private readonly IConfiguration _configuration;

        public ApplicationContext(IConfiguration configuration)
        {
            Database.EnsureCreated();
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeOrganizationMap>()
                .HasKey(eom => new { eom.loginEmp, eom.orgId });
        }
    }
}
