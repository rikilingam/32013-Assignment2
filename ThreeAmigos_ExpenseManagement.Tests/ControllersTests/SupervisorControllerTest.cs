using System;
using System.Collections.Generic;
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
    public class SupervisorControllerTest
    {
        IExpenseReportService mockReportService = new MockExpenseReportService();
        IEmployeeService mockEmployeeService = new MockEmployeeService();
        Employee mockEmployee;
        static Department dept = new Department { DepartmentId = 1,DepartmentName="State Services",MonthlyBudget=10000 };


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
            SupervisorController controller=new SupervisorController(mockEmployeeService,mockReportService,mockEmployee,mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result=controller.ViewReports();

            Assert.IsInstanceOfType(result,typeof(ActionResult),"Result is not of ActionResult type");


        }

        [TestMethod]
        public void ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "ViewReports";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
          
            var result = controller.ViewReports() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }
        
        [TestMethod]
        public void HttpPost_ViewReports_Returns_ActionResult()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString());

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");


        }

        [TestMethod]
        public void HttpPost_ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "ViewReports";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void HttpPost_ViewReports_ViewData_IsListOfExpenseReports()
        {

            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<ExpenseReport>));
        }

        [TestMethod]
        public void HttpPost_ViewReports_CheckDepartmentIdOfReportAndEmployee_AreEqual()
        {
            Employee mockEmployee = mockEmployeeService.GetEmployee(1);
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;
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
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;
            var ExpenseReports = (List<ExpenseReport>)result.Model;
            foreach (var report in ExpenseReports)
            {
                Assert.AreEqual(ReportStatus.Submitted.ToString(), report.Status);
            }
        }

        [TestMethod]
        public void ViewReports_ReportsAreDisplayedForCurrentMonthOfCurrentYear_IsTrue()
        {
            Employee mockEmployee = mockEmployeeService.GetEmployee(1);
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;
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
            const string expectedViewName = "CheckBalance";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.CheckBalance() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void CheckBalance_ViewData_IsBudget()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.CheckBalance() as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Budget));
        }

        [TestMethod]
        public void CheckBalance_CorrectMoneyRemaining_IsReturned()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.CheckBalance() as ViewResult;
            var budget = (Budget)result.Model;

            decimal? MoneyRemaining = 5500;

            Assert.AreEqual(MoneyRemaining, budget.RemainingBudget);
        }

        [TestMethod]
        public void CheckBalance_CorrectAmountSpent_IsReturned()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.CheckBalance() as ViewResult;
            var budget = (Budget)result.Model;

            decimal? AmountSpent = 4500;

            Assert.AreEqual(AmountSpent, budget.Spent);
        }

                [TestMethod]
        public void ApproveExpenses_Returns_ActionResult()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ApproveExpenses();

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");
        
        }

        [TestMethod]
        public void ApproveExpenses_Returns_View_ApproveExpenses()
        {
            const string expectedViewName ="ApproveExpenses";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ApproveExpenses() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void ApproveExpenses_ViewData_IsApproveExpensesViewModel()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.ApproveExpenses() as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ApproveExpensesViewModel));
        }



        [TestMethod]
        public void ApproveExpense_Returns_ActionResult()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ApproveExpense(1,"Approve");

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");

        }

        [TestMethod]
        public void ApproveExpense_Returns_View_ApproveExpenses()
        {
            const string expectedViewName = "ApproveExpenses";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ApproveExpense(1, "Approve") as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void ApproveExpense_ViewData_IsApproveExpensesViewModel()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetService);
            MockHttpContext.SetFakeHttpContext(controller);
            var result = controller.ApproveExpense(1, "Approve") as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(ApproveExpensesViewModel));
        }

        
    }
}
