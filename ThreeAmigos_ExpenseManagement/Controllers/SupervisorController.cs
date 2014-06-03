using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Filters;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.ViewModels;

namespace ThreeAmigos_ExpenseManagement.Controllers
{
    [Authorize(Roles = "Supervisor")]
    [InitializeSimpleMembership]
    public class SupervisorController : Controller
    {
        private IBudgetService deptBudget;        
        private IExpenseReportService reportService;
        private IEmployeeService employeeService;
        private Employee employee;

        readonly DateTime TODAY = DateTime.Now;

        public SupervisorController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
            deptBudget = new DepartmentBudgetService(employee.Department);
            deptBudget.SetBudgetSpent(TODAY.Month, TODAY.Year);
        }

         public SupervisorController(IEmployeeService empService, IExpenseReportService rptService, Employee employee,IBudgetService budget)
        {
            employeeService = empService;
            reportService = rptService;
            this.employee = employee;
            deptBudget = budget;  
         }

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult ApproveExpenses()
        {
            ApproveExpensesViewModel expenses = new ApproveExpensesViewModel();
            expenses.BudgetTracker = deptBudget;
            expenses.ExpenseReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());
            return View("ApproveExpenses",expenses);
        }

        /// <summary>
        ///  Approves or rejects the report on the basis of action selected
        /// </summary>
        public ActionResult ApproveExpense(int? expenseId, string status)
        {
            ApproveExpensesViewModel expenses = new ApproveExpensesViewModel();
             
                reportService.ActionOnReport(expenseId, employee,(ReportStatus)Enum.Parse(typeof(ReportStatus),status));
                expenses.ExpenseReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());
                deptBudget.SetBudgetSpent(TODAY.Month, TODAY.Year);
                expenses.BudgetTracker = deptBudget;
                return RedirectToAction("ApproveExpenses");
        }

        public ActionResult ViewReports()
        {
            return View("ViewReports");
        }


        /// <summary>
        /// Filters the list of expense reports by status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewReports(string status)
        {
            List<ExpenseReport> expReports = new List<ExpenseReport>();
            expReports = reportService.GetReportsBySupervisor(status);
            return View("ViewReports",expReports);
        }


        /// <summary>
        ///  Checks the balance left of from the monthly budget
        /// </summary>
        public ActionResult CheckBalance()
        {
            return View("CheckBalance",deptBudget.Budget);
        }
    }

}
