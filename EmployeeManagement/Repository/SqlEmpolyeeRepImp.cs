using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repository
{
    public class SqlEmpolyeeRepImp : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public SqlEmpolyeeRepImp(ApplicationDbContext context)
        {
            _context = context;
        }

        public Employee AddEmployee(Employee employee)
        {
            
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            var allEmployee = _context.Employees.ToList();
            return allEmployee;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }

        public Employee UpdateEmployee(Employee employee)
        {

            var employe = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);
            //if (employe != null)
            //{
            //    employe.Id = employee.Id;
            //    employe.Name = employee.Name;
            //    employe.Email = employee.Email;
            //    employe.Department = employee.Department;
            //}
            _context.Employees.Update(employe);
            _context.SaveChanges();
            return employe;
        }
    }
}
