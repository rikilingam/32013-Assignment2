using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Filters;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.ViewModels;


namespace ThreeAmigos_ExpenseManagement.Controllers
{
    public class AccountsController : Controller
    {
        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            return View();
        }

        IExpenseReportService reportService = new ExpenseReportService();
        private IEmployeeService employeeService;
        private Employee employee;
        BudgetTracker bud;
        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;


        public AccountsController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            bud = new BudgetTracker();
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
        }


        public ActionResult ProcessExpenses(int? itemid, string act)
        {
            ProcessExpensesViewModel expenseForm = new ProcessExpensesViewModel();

            if (itemid == null)
            {
                expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("ApprovedBySupervisor");
                return View(expenseForm);
            }
            else
            {
                reportService.ProcessReport(itemid, act);
                expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("ApprovedBySupervisor");
                return View(expenseForm);
            }
        }


        public ActionResult ViewReports()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ViewReports(string status)
        {
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            expenseForm.ExpenseReports = reportService.GetReportsBySupervisor(status);
            return View(expenseForm);
        }


        public ActionResult CheckExpenseApproved()
        {
            Budget budget = new Budget();
            budget.companyAmountSpent = bud.CompanyBudgetRemain(month, year);
            budget.companyAmountRemaining = bud.RemainingAmount - bud.CompanyBudgetRemain(month, year);
            return View(budget);
        }

    }
}
