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

        IExpenseReportService reportService = new ExpenseReportService();
        private IEmployeeService employeeService;
        private Employee employee;
        IBudgetTracker budgetTracker;
        IConfigurationDAL config;
        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;

        public AccountsController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            //budgetTracker = new BudgetTracker();
            config = new ConfigurationDAL();            
            budgetTracker = new CompanyBudgetService(decimal.Parse((string)config.GetAppSetting("CompanyMonthlyBudget")));
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
        }


        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult ProcessExpenses(int? itemid, string act)
        {
            ProcessExpensesViewModel expenseForm = new ProcessExpensesViewModel();

            if (itemid == null)
            {
                expenseForm.ExpenseReports = reportService.GetReportsByAccounts(ReportStatus.ApprovedBySupervisor.ToString());   // "ApprovedBySupervisor");
                expenseForm.BudgetTracker = budgetTracker;
                expenseForm.BudgetTracker.SetBudgetSpent(month,year);
                return View(expenseForm);
            }
            else
            {
                reportService.ProcessReport(itemid, act);
                expenseForm.ExpenseReports = reportService.GetReportsByAccounts(ReportStatus.ApprovedBySupervisor.ToString()); // "ApprovedBySupervisor");
                expenseForm.BudgetTracker = budgetTracker;
                expenseForm.BudgetTracker.SetBudgetSpent(month, year);
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
            List<ExpenseReport> expReports = new List<ExpenseReport>();
            expReports = reportService.GetReportsByAccounts(status);
            return View(expReports);
        }


        public ActionResult CheckExpenseApproved()
        {
            List<AmountProcessedSupervisor> report = new List<AmountProcessedSupervisor>();
            report = reportService.GetAmountSupervisor();
            return View(report);
        }

    }
}
