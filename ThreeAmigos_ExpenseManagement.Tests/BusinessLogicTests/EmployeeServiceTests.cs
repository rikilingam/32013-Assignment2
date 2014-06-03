using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Tests.Mocks;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.Tests.BusinessLogicTests
{
    [TestClass]
    public class EmployeeServiceTests
    {
        IEmployeeService employeeService;

        [TestInitialize]
        public void InitializeEmployeeService()
        {
            IEmployeeDAL employeeDAL = new MockEmployeeDAL();

            employeeService = new EmployeeService(employeeDAL);
        }

        [TestMethod]
        public void EmployeeService_GetEmployee_AreEqual()
        {
            Employee expectedEmployee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

            Employee result = employeeService.GetEmployee(1);

            Assert.AreEqual(expectedEmployee.UserId, result.UserId, "Expected employee and employee from employeeservice are not equal");
        }

        [TestMethod]
        public void EmployeeService_GetEmployee_AreNotEqual()
        {
            Employee expectedEmployee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

            Employee result = employeeService.GetEmployee(2);

            Assert.AreNotEqual(expectedEmployee.UserId, result.UserId, "Expected employee and employee from employeeservice are equal");
        }

        [TestMethod]
        [ExpectedExceptionAttribute(typeof(System.FormatException))]
        public void EmployeeService_GetEmployeeUserIdasString_ThrowFormatException()
        {
            Employee expectedEmployee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

            var testId = "John";
            Employee result = employeeService.GetEmployee(Int32.Parse(testId));

            Assert.AreEqual(expectedEmployee.UserId, result.UserId, "This should throw an expception as GetEmployee does not expect a string");       
        }
    }
}
