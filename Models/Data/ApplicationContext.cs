using Microsoft.EntityFrameworkCore;

namespace ManageStaff.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ManageStaffDB;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Server=DESKTOP-57NA4NE\\SQLEXPRESS;Database=ManageStaffDB;Trusted_Connection=True;");
        }
    }
}
