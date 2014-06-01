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
        IBudgetTracker mockBudgetTracker = new MockBudgetTracker();
     
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
            SupervisorController controller=new SupervisorController(mockEmployeeService,mockReportService,mockEmployee,mockBudgetTracker);
            MockHttpContext.SetFakeHttpContext(controller);

            var result=controller.ViewReports();

            Assert.IsInstanceOfType(result,typeof(ActionResult),"Result is not of ActionResult type");


        }

        [TestMethod]
        public void ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "ViewReports";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetTracker);
            MockHttpContext.SetFakeHttpContext(controller);
          
            var result = controller.ViewReports() as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }
        
        [TestMethod]
        public void HttpPost_ViewReports_Returns_ActionResult()
        {
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetTracker);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString());

            Assert.IsInstanceOfType(result, typeof(ActionResult), "Result is not of ActionResult type");


        }

        [TestMethod]
        public void HttpPost_ViewReports_Returns_View_ViewReports()
        {
            const string expectedViewName = "ViewReports";
            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetTracker);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;

            Assert.AreEqual(expectedViewName, result.ViewName, "View names do not match, expected view name is{0}", expectedViewName);
        }

        [TestMethod]
        public void HttpPost_ViewReports_ViewData()
        {

            SupervisorController controller = new SupervisorController(mockEmployeeService, mockReportService, mockEmployee, mockBudgetTracker);
            MockHttpContext.SetFakeHttpContext(controller);

            var result = controller.ViewReports(ReportStatus.Submitted.ToString()) as ViewResult;

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<ExpenseReport>));
        }
    }
}
