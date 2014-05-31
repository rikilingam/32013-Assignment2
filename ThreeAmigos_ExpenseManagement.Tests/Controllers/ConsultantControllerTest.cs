using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos_ExpenseManagement.Controllers;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using System.Web.Mvc;
using ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.ViewModels;

namespace ThreeAmigos_ExpenseManagement.Tests.Controllers
{
    [TestClass]
    public class ConsultantControllerTest
    {
        IExpenseReportService reportService = new MockExpenseReportService();
        IEmployeeService employeeService = new MockEmployeeService();

        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void TestMethod1()
        //{
        //}

        [TestMethod]
        public void CreateExpense_CheckEmployeeIsLoadedWithDepartment_IsEqual()
        {
            Employee mockEmployee = employeeService.GetEmployee(1); //get the employee from the mock employee service

            Employee employee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

            Assert.AreEqual(employee.UserId, mockEmployee.UserId, "Employee user id's are not equal");
            Assert.AreEqual(employee.Firstname, mockEmployee.Firstname, "Employee firstnames are not equal");
            Assert.AreEqual(employee.Surname, mockEmployee.Surname, "Employee surnames are not equal");
            Assert.AreEqual(employee.Department.DepartmentId, mockEmployee.Department.DepartmentId, "Employee departments are not equal");
        }

        [TestMethod]
        public void CreateExpense_CheckInitialExpenseReportIsLoaded_IsEqual()
        {           

            ConsultantController controller = new ConsultantController(employeeService, reportService, 1);
            
            MockHttpContext.SetFakeAuthenticatedControllerContext(controller);

            ViewResult result = controller.CreateExpense() as ViewResult;

            Employee employee = employeeService.GetEmployee(1);
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = employee;
            expenseReport.Department = employee.Department;

            expenseForm.ExpenseReport = expenseReport;            

            ExpenseFormViewModel model = (ExpenseFormViewModel)result.ViewData.Model;
                        
            Assert.AreEqual(expenseReport, expenseForm.ExpenseReport, "Expense report is not equal");
        }

        [TestMethod]
        public void CreateExpense_RetrieveExpenseReport_IsEqual()
        {

        }
    }
}
