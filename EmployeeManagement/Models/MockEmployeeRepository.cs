namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;
        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee(){Id = 1, Name="Abaseen", Department=Dept.Hr, Email="Ab@gmail.com"},
                new Employee(){Id = 2, Name="Abaseen2", Department=Dept.IT, Email="Ab2@gmail.com"},
                new Employee(){Id = 3, Name="Abaseen3", Department=Dept.Payroll, Email="Ab3@gmail.com"},
            };
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _employees.Max(x => x.Id)+1;
            _employees.Add(employee);
             return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = _employees.FirstOrDefault(x => x.Id == id);
            if(employee != null)
            {
                _employees.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            var allEmployee = _employees;
            return allEmployee;
        }

        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public Employee UpdateEmployee(Employee employee)
        {

            var employe = _employees.FirstOrDefault(x => x.Id == employee.Id);
            if(employe != null)
            {
                employe.Id = employee.Id;
                employe.Name = employee.Name;
                employe.Email = employee.Email;
                employe.Department = employee.Department;
            }
            return employe;
        }
    }
}
