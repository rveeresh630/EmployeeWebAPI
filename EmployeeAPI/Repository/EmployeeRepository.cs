using BethanypieShopsHRM.Api.Repository;

using EmployeeAPI.Models;

namespace EmployeeAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        private Random random = new Random();

        public EmployeeRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Employee AddEmployee(Employee employee)
        {
            var addedEntity = _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public void DeleteEmployee(int employeeId)
        {
            Employee foundEmployee = _appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (foundEmployee == null) 
                return ;

            _appDbContext.Employees.Remove(foundEmployee);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employee = _appDbContext.Employees;
            foreach(var e in employee)
            {
                e.Password = "";
            }
            return employee;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var employee = _appDbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);
            if(employee != null)
            {
                employee.Password = "";

            }            
            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            Employee foundEmployee =  _appDbContext.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);

            if (foundEmployee != null)
            {
                foundEmployee.EmployeeId = employee.EmployeeId;
                foundEmployee.Email = employee.Email;
                foundEmployee.FirstName = employee.FirstName;
                foundEmployee.LastName = employee.LastName;
                 _appDbContext.SaveChanges();

                return foundEmployee;
            }

            return null;
        }
    }
}
