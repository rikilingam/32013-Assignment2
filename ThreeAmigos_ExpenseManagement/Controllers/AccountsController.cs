﻿using System;
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
    [Authorize(Roles = "Accounts")]
    [InitializeSimpleMembership]
    public class AccountsController : Controller
    {

        IExpenseReportService reportService = new ExpenseReportService();
        private IEmployeeService employeeService;
        private Employee employee;
        IBudgetService budgetTracker;
        IConfigurationDAL config;

        readonly DateTime TODAY = DateTime.Now;
        readonly string COMPANY_BUDGET_KEY = "CompanyMonthlyBudget";
        
        public AccountsController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            config = new ConfigurationDAL();            
            budgetTracker = new CompanyBudgetService(decimal.Parse((string)config.GetAppSetting(COMPANY_BUDGET_KEY)));
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
        }


        public AccountsController(IEmployeeService empService, IExpenseReportService rptService, Employee employee,IBudgetService budget)
        {
            employeeService = empService;
            reportService = rptService;
            this.employee = employee;
            budgetTracker = budget;  
         }


        //
        // GET: /Accounts/

        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult ProcessExpenses()
        {
            ProcessExpensesViewModel expenseForm = new ProcessExpensesViewModel();
            
            expenseForm.ExpenseReports = reportService.GetReportsByAccounts(ReportStatus.ApprovedBySupervisor.ToString());
            expenseForm.BudgetTracker = budgetTracker;
            expenseForm.BudgetTracker.SetBudgetSpent(TODAY.Month, TODAY.Year);
            return View("ProcessExpenses", expenseForm);

        }

        /// <summary>
        ///  Processes the report on the basis of action selected
        /// </summary>
        public ActionResult ProcessExpenseItem(int expenseId, string status)
        {
            ProcessExpensesViewModel expenses = new ProcessExpensesViewModel();

            reportService.ProcessReport(expenseId, employee, (ReportStatus)Enum.Parse(typeof(ReportStatus), status));
            //expenses.ExpenseReports = reportService.GetReportsBySupervisor(ReportStatus.ApprovedBySupervisor.ToString());
            //budgetTracker.SetBudgetSpent(DateTime.Now.Month, DateTime.Now.Year);
            //expenses.BudgetTracker = budgetTracker;
            return RedirectToAction("ProcessExpenses");
            //return View("ProcessExpenses", expenses);
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
            expReports = reportService.GetReportsByAccounts(status);
            return View("ViewReports", expReports);
        }



        /// <summary>
        /// Checks the amount approved by each supervisor
        /// </summary>
       public ActionResult CheckExpenseApproved()
        {
            List<AmountProcessedSupervisor> report = new List<AmountProcessedSupervisor>();
            report = reportService.GetAmountSupervisor();
            return View("CheckExpenseApproved", report);
        }

    }
}
