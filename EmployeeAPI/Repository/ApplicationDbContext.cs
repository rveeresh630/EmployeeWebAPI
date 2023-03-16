using EmployeeAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace BethanypieShopsHRM.Api.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                FirstName="Richard",
                LastName="Willams",
                Email = "RichardWillams@gmail.com",
                Password="123456"
            });
            modelBuilder.Entity<Employee>().HasData(new Employee() 
            {
                EmployeeId = 2,
                FirstName = "Will",
                LastName = "Sams",
                Email = "WillSams@gmail.com",
                Password = "123456"
            });
        }

    }
}
