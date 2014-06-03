using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.Tests.Mocks
{
    public class MockEmployeeDAL:IEmployeeDAL
    {
        public Employee GetEmployee(int userId)
        {
            var result = (from employee in GetFakeEmployees()
                         where employee.UserId == userId
                         select employee).FirstOrDefault();
                       
            return result;
        }

        private List<Employee> GetFakeEmployees()
        {

            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

            employees.Add(employee);

            employee = new Employee();
            employees.Add(employee);

            employee = new Employee
            {
                UserId = 2,
                Firstname = "Vikki",
                Surname = "Car",
                Department = new Department { DepartmentId = 2, DepartmentName = "Test Department", MonthlyBudget = 10000 },
                Role = "Supervisor"
            };

            employees.Add(employee);

            return employees;

        }
    }
}
