using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.ModelBuilderExtensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Abaseen",
                   Email = "A@gmail.com",
                   Department = Dept.IT,
               }
           );
            modelBuilder.Entity<Employee>().HasData(
              new Employee
              {
                  Id = 2,
                  Name = "Rafi",
                  Email = "A@gmail.com",
                  Department = Dept.Payroll,
              }
          );
        }
    }
}
