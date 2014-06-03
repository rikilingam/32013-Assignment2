using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos_ExpenseManagement.Controllers;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using System.Web.Mvc;
using ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.ViewModels;

namespace ThreeAmigos_ExpenseManagement.Tests.ControllersTests
{
    [TestClass]
    class AccountsControllerTest
    {
        IExpenseReportService mockReportService = new MockExpenseReportService();
        IEmployeeService mockEmployeeService = new MockEmployeeService();
        Employee mockEmployee;
        static Department dept = new Department { DepartmentId = 1, DepartmentName = "State Services", MonthlyBudget = 10000 };


        IBudgetService mockBudgetService = new MockBudgetService(dept);

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

        [TestMethod]
        public void ViewReports_Returns_ActionResult()
        {
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports();

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");


        }

        [TestMethod]
        public void ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "ViewReports";
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void HttpPost_ViewReports_Returns_ActionResult()
        {
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.ApprovedBySupervisor.ToString());

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");


        }

        [TestMethod]
        public void HttpPost_ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "ViewReports";
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.ApprovedBySupervisor.ToString()) as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void HttpPost_ViewReports_ViewData_IsListOfExpenseReports()
        {

            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.ApprovedBySupervisor.ToString()) as ViewResult;

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<ExpenseReport>));
        }

        [TestMethod]
        public void HttpPost_ViewReports_CheckDepartmentIdOfReportAndEmployee_AreEqual()
        {
            Employee mockEmployee = mockEmployeeService.GetEmployee(1);
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.ApprovedBySupervisor.ToString()) as ViewResult;
            var ExpenseReports = (List<ExpenseReport>)result.Model;
            foreach (var report in ExpenseReports)
            {
                Assert.AreEqual(mockEmployee.Department.DepartmentId, report.ExpenseToDept);
            }
        }

        [TestMethod]
        public void HttpPost_ViewReports_CheckStatusOfReportAndStatusPassed_AreEqual()
        {
            Employee mockEmployee = mockEmployeeService.GetEmployee(1);
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.ApprovedBySupervisor.ToString()) as ViewResult;
            var ExpenseReports = (List<ExpenseReport>)result.Model;
            foreach (var report in ExpenseReports)
            {
                Assert.AreEqual(ReportStatus.ApprovedBySupervisor.ToString(), report.Status);
            }
        }

        [TestMethod]
        public void ViewReports_ReportsAreDisplayedForCurrentMonthOfCurrentYear_IsTrue()
        {
            Employee mockEmployee = mockEmployeeService.GetEmployee(1);
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.ViewReports(ReportStatus.ApprovedBySupervisor.ToString()) as ViewResult;
            var ExpenseReports = (List<ExpenseReport>)result.Model;
            foreach (var report in ExpenseReports)
            {
                Assert.IsTrue(month == report.CreateDate.Value.Month);
                Assert.IsTrue(year == report.CreateDate.Value.Year);
            }
        }


        [TestMethod]
        public void CheckBalance_Returns_View_CheckBalance()
        {
            const string expectedViewName = "CheckExpenseApproved";
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.CheckExpenseApproved() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void CheckBalance_ViewData_IsBudget()
        {
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.CheckExpenseApproved() as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Budget));
        }

        [TestMethod]
        public void ApproveExpenses_Returns_ActionResult()
        {
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ProcessExpenses();

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");

        }

        [TestMethod]
        public void ApproveExpenses_Returns_View_ApproveExpenses()
        {
            const string expectedViewName = "ProcessExpenses";
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ProcessExpenses() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void ApproveExpenses_ViewData_IsApproveExpensesViewModel()
        {
            AccountsController controller = new AccountsController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.ProcessExpenses() as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ApproveExpensesViewModel));
        }      

    }
}
