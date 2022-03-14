
namespace EmployeeDbApp.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cites { get; set; }
        public DbSet<Employee> Employees { get; set; }




    }
}
