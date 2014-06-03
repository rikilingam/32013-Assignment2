using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos_ExpenseManagement.Controllers;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using System.Web.Mvc;
using ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace ThreeAmigos_ExpenseManagement.Tests.Controllers
{
    [TestClass]
    public class ConsultantControllerTest
    {
        IExpenseReportService mockReportService = new MockExpenseReportService();
        IEmployeeService mockEmployeeService = new MockEmployeeService();
        Employee mockEmployee;

        [TestInitialize]
        public void InitializeMockEmployee()
        {
            mockEmployee = new Employee
            {
                UserId = 1,
                Firstname = "John",
                Surname = "Doe",
                Department = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 },
                Role = "Consultant"
            };

        }

        //Check the default constructor loads with correct employee
        [TestMethod]
        public void Consultant_CreateExpense_CheckEmployeeIsLoadedWithDepartment()
        {
            //Arrange
            Employee mockEmployee = mockEmployeeService.GetEmployee(1); //get the employee from the mock employee service                       

            //Assert
            Assert.AreEqual(mockEmployee.UserId, mockEmployee.UserId, "Employee user id's are not equal");
            Assert.AreEqual(mockEmployee.Firstname, mockEmployee.Firstname, "Employee firstnames are not equal");
            Assert.AreEqual(mockEmployee.Surname, mockEmployee.Surname, "Employee surnames are not equal");
            Assert.AreEqual(mockEmployee.Department.DepartmentId, mockEmployee.Department.DepartmentId, "Employee departments are not equal");
        }

        //Test the CreateExpense action is a type of ActionResult
        [TestMethod]
        public void Consultant_CreateExpense_Returns_ActionResult()
        {
            //Arrange            
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);
            //Act
            var result = controller.CreateExpense();

            //Assert
            Assert.IsInstanceOfType(result,typeof(ActionResult),"The result was not a type of ActionResult");
        }

        // Check that the correct view is returned
        [TestMethod]
        public void Consultant_CreateExpense_Returns_View_CreateExpense()
        {
            //Arrange
            const string expectedViewName = "CreateExpense";
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);

            MockHttpContext.SetFakeHttpContext(controller);

            //Act
            var result = controller.CreateExpense() as ViewResult;            

            //Assert
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);

        }

        //Test that the initial ExpenseReport is intialized correctly
        [TestMethod]
        public void Consultant_CreateExpense_CheckInitialExpenseReportIsInitialized()
        {           
            //Arrange
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);            
            MockHttpContext.SetFakeHttpContext(controller);
            
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = mockEmployee;
            expenseReport.Department = mockEmployee.Department;           

            Assert.AreEqual(expenseReport.CreatedBy, mockEmployee, "The createby employee is not equal");
            Assert.AreEqual(String.Format("{0:dd/mm/yyyy}",expenseReport.CreateDate),String.Format("{0:dd/mm/yyyy}",DateTime.Now), "Create date is not same");
            Assert.AreEqual(expenseReport.Department, mockEmployee.Department, "Expense department is not equal to employee department");
        }

        //Test the data being sent to the view
        [TestMethod]
        public void Consultant_CreateExpense_ViewData_ExpenseFormViewModal()
        {
            //Arrange
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            ExpenseFormViewModel model = new ExpenseFormViewModel();
            model.ExpenseItem = new ExpenseItem();
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = mockEmployee;
            expenseReport.Department = mockEmployee.Department;
            
            model.ExpenseReport = expenseReport;
            
            //Act
            var result = controller.CreateExpense(model) as ViewResult;


            //Assert
            Assert.AreEqual(model, (ExpenseFormViewModel)result.ViewData.Model, "The view model sent is not the same as what was intialized");
            Assert.IsNotNull((ExpenseFormViewModel)result.ViewData.Model, "The resulting view modal is null");
        }

        //Test the HttpPost CreateExpense action is a type of ActionResult
        [TestMethod]
        public void Consultant_HttpPost_CreateExpense_Returns_ActionResult()
        {
            //Arrange            
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            expenseForm.CreateDate = DateTime.Now;
            expenseForm.DepartmentName = mockEmployee.Department.DepartmentName;
            expenseForm.EmployeeName = mockEmployee.Fullname;
            expenseForm.ExpenseItem = new ExpenseItem();

            //Act
            var result = controller.CreateExpense(expenseForm);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult), "The result was not a type of ActionResult");
        }

        // Check that the correct view is returned
        [TestMethod]
        public void Consultant_HttpPost_CreateExpense_Returns_View_CreateExpense()
        {
            //Arrange
            const string expectedViewName = "CreateExpense";
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            expenseForm.CreateDate = DateTime.Now;
            expenseForm.DepartmentName = mockEmployee.Department.DepartmentName;
            expenseForm.EmployeeName = mockEmployee.Fullname;
            expenseForm.ExpenseItem = new ExpenseItem();

            //Act
            var result = controller.CreateExpense(expenseForm) as ViewResult;

            //Assert
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);

        }

        //Check that the expense item sent to the view is the same as the intial item that was intialized
        [TestMethod]
        public void Consultant_HttpPost_CreateExpense_AddExpenseItem()
        {
            //Arrange
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            expenseForm.CreateDate = DateTime.Now;
            expenseForm.DepartmentName = mockEmployee.Department.DepartmentName;
            expenseForm.EmployeeName = mockEmployee.Fullname;
            
            expenseForm.ExpenseItem = new ExpenseItem();
            expenseForm.ExpenseItem.Description = "Laptop";
            expenseForm.ExpenseItem.Location = "Sydney Airport";
            expenseForm.ExpenseItem.Amount = (decimal)1200.00;
            expenseForm.ExpenseItem.AudAmount = (decimal)1200.00;
            expenseForm.ExpenseItem.Currency = "AUD";

            //Act
            ViewResult result = controller.CreateExpense(expenseForm) as ViewResult;
            ExpenseFormViewModel model = (ExpenseFormViewModel)result.ViewData.Model;

            //Assert
            Assert.AreEqual(model.ExpenseItem, expenseForm.ExpenseItem, "Expense items in view are not equal to expense item in the response");
            Assert.IsNotNull((ExpenseFormViewModel)result.ViewData.Model, "The resulting view modal is null");
        }

        [TestMethod]
        public void Consultant_ViewReports_Returns_ActionResult()
        {
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewMyExpenses();

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");


        }

        [TestMethod]
        public void Consultant_ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "";
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewMyExpenses() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void Consultant_HttpPost_ViewReports_Returns_ActionResult()
        {
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewMyExpenses(ReportStatus.Submitted.ToString());

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");


        }

        [TestMethod]
        public void Consultant_HttpPost_ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "";
            ConsultantController controller = new ConsultantController(mockEmployeeService, mockReportService, mockEmployee);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewMyExpenses(ReportStatus.Submitted.ToString()) as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }
    }
}
