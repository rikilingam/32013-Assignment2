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
    public class SupervisorController : Controller
    {
        private IBudgetService deptBudget;        
        private IExpenseReportService reportService;
        private IEmployeeService employeeService;
        private Employee employee;      

        public SupervisorController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
            //deptBudget = new BudgetTracker(employee.Department);
            deptBudget = new DepartmentBudgetService(employee.Department);
            deptBudget.SetBudgetSpent(DateTime.Now.Month, DateTime.Now.Year);   
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
            return View(expenses);
        }

        public ActionResult ApproveExpense(int? itemid, string act)
        {
            ApproveExpensesViewModel expenses = new ApproveExpensesViewModel();
            if (act == "Approve")
            {                
                reportService.ActionOnReport(itemid, act);
                expenses.ExpenseReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());
                deptBudget.SetBudgetSpent(DateTime.Now.Month, DateTime.Now.Year);
                expenses.BudgetTracker = deptBudget;                                
                return View("ApproveExpenses", expenses);
            }
            else
            {
                reportService.ActionOnReport(itemid, act);
                expenses.ExpenseReports = reportService.GetReportsBySupervisor(ReportStatus.Submitted.ToString());
                deptBudget.SetBudgetSpent(DateTime.Now.Month, DateTime.Now.Year);
                expenses.BudgetTracker = deptBudget;                  
                return View("ApproveExpenses", expenses);
            }
        }

        public ActionResult ViewReports()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ViewReports(string status)
        {
            List<ExpenseReport> expReports = new List<ExpenseReport>();
            expReports = reportService.GetReportsBySupervisor(status);
            return View(expReports);
        }

        public ActionResult CheckBalance()
        {
            //Budget budget = new Budget();
            //budget.totalAmountRemaining = deptBudget.RemainingAmount;
            //budget.totalAmountSpent = deptBudget.TotalExpenseAmount;
            return View(deptBudget.Budget);
        }
    }

}
