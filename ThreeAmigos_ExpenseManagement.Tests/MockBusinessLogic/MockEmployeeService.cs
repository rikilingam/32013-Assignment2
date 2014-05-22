using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic
{
    public class MockEmployeeService:IEmployeeService
    {
        public Employee GetEmployee(int userId)
        {
            Employee employee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department {DepartmentId=1,DepartmentName="State Services",MonthlyBudget=10000 },
                Role="Consultant"
            };

            return employee;
        }
    }
}
