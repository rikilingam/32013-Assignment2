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
        IExpenseReportService reportService = new MockExpenseReportService();
        IEmployeeService employeeService = new MockEmployeeService();

        [TestMethod]
        public void ViewReports_CheckDepartmentIdOfReportAndEmployee_AreEqual()
        {
            Employee mockEmployee = employeeService.GetEmployee(1); 

            List<ExpenseReport> expReports = new List<ExpenseReport>();
            expReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());

            foreach (var report in expReports)
            {
                Assert.AreEqual(mockEmployee.Department.DepartmentId, report.ExpenseToDept);
            }
        }

        [TestMethod]
        public void ViewReports_CheckStatusOfReportAndStatusPassed_AreEqual()
        {
            Employee mockEmployee = employeeService.GetEmployee(1); 

            List<ExpenseReport> expReports = new List<ExpenseReport>();
            expReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());

            foreach (var report in expReports)
            {
                Assert.AreEqual(ReportStatus.Submitted.ToString(), report.Status);
            }
        }

        [TestMethod]
        public void ViewReports_ReportsAreDisplayedForCurrentMonthOfCurrentYear_IsTrue()
        {
            Employee mockEmployee = employeeService.GetEmployee(1);
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            List<ExpenseReport> expReports = new List<ExpenseReport>();
            expReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());

            foreach (var report in expReports)
            {
                Assert.IsTrue(month == report.CreateDate.Value.Month);
                Assert.IsTrue(year == report.CreateDate.Value.Year);
            }
        }


    }
}
