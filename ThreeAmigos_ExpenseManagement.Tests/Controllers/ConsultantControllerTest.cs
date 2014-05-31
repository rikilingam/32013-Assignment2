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



        [TestMethod]
        public void CreateExpense_CheckEmployeeIsLoadedWithDepartment_IsEqual()
        {
            //Arrange
            Employee mockEmployee = employeeService.GetEmployee(1); //get the employee from the mock employee service
                        
            Employee employee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

            //Assert
            Assert.AreEqual(employee.UserId, mockEmployee.UserId, "Employee user id's are not equal");
            Assert.AreEqual(employee.Firstname, mockEmployee.Firstname, "Employee firstnames are not equal");
            Assert.AreEqual(employee.Surname, mockEmployee.Surname, "Employee surnames are not equal");
            Assert.AreEqual(employee.Department.DepartmentId, mockEmployee.Department.DepartmentId, "Employee departments are not equal");
        }

        //Test the CreateExpense action is a type of ActionResult
        [TestMethod]
        public void CreateExpense_Returns_ActionResult()
        {
            //Arrange            
            ConsultantController controller = new ConsultantController(employeeService, reportService, 1);
            MockHttpContext.SetFakeAuthenticatedControllerContext(controller);
            //Act
            var result = controller.CreateExpense();

            //Assert
            Assert.IsInstanceOfType(result,typeof(ActionResult),"The result was not a type of ActionResult");
        }

        //Test that the initial ExpenseReport is intialized correctly
        [TestMethod]
        public void CreateExpense_CheckInitialExpenseReportIsInitialized_IsEqual()
        {           
            //Arrange
            ConsultantController controller = new ConsultantController(employeeService, reportService, 1);            
            MockHttpContext.SetFakeAuthenticatedControllerContext(controller);
            //ViewResult result = controller.CreateExpense() as ViewResult;
            Employee employee = employeeService.GetEmployee(1);
            //ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = employee;
            expenseReport.Department = employee.Department;

            //expenseForm.ExpenseReport = expenseReport;            

            //ExpenseFormViewModel model = (ExpenseFormViewModel)result.ViewData.Model;

            Assert.AreEqual(expenseReport, expenseForm.ExpenseReport, "Expense report is not equal");
        }

        [TestMethod]
        public void CreateExpense_Returns_View_ExpenseView()
        {
            //Arrange
            const string expectedViewName="CreateExpense";
            ConsultantController controller = new ConsultantController(employeeService, reportService, 1);

            MockHttpContext.SetFakeAuthenticatedControllerContext(controller);

            Employee employee = employeeService.GetEmployee(1);
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = employee;
            expenseReport.Department = employee.Department;

            expenseForm.ExpenseReport = expenseReport;

            //Act
            var result = controller.CreateExpense() as ViewResult;
            //ExpenseFormViewModel model = (ExpenseFormViewModel)result.ViewData.Model;

            //Assert
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);

        }



        [TestMethod]
        public void CreateExpense_RetrieveExpenseReport_IsEqual()
        {

        }
    }
}
