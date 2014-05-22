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

    [Authorize(Roles = "Consultant")]
    [InitializeSimpleMembership]
    public class ConsultantController : Controller
    {
        private IExpenseReportService reportService;
        private IEmployeeService employeeService;
        private Employee employee;

        public ConsultantController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
        }

        public ConsultantController(IEmployeeService empService, IExpenseReportService rptService, int userId)
        {           
            employeeService = empService;
            reportService = rptService;
            employee = empService.GetEmployee(userId);
        }

        //
        // GET: /Consultant/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This controllers initializes the expense form
        /// </summary>
        /// <returns>returns create expense view</returns>
        public ActionResult CreateExpense()
        {
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = employee;
            expenseReport.Department = employee.Department;

            Session["_expenseReport"] = expenseReport;

            expenseForm.ExpenseReport = expenseReport;
            return View(expenseForm);

        }

        /// <summary>
        /// This controller adds items to the expense form
        /// </summary>
        /// <param name="expenseForm">the completed expense item</param>
        /// <returns>The expense form with expense items</returns>
        [HttpPost]
        public ActionResult CreateExpense(ExpenseFormViewModel expenseForm)
        {
            if (ModelState.IsValid)
            {
                ExpenseReport expenseReport = new ExpenseReport();

                expenseForm.ExpenseItem.AudAmount = CurrencyService.CalcAud(expenseForm.ExpenseItem.Amount, expenseForm.ExpenseItem.Currency);

                if (Session["_expenseReport"] != null)
                {
                    expenseReport = (ExpenseReport)Session["_expenseReport"];
                    expenseReport.ExpenseItems.Add(expenseForm.ExpenseItem);
                    Session["_expenseReport"] = expenseReport;
                }
                else
                {
                    expenseReport.ExpenseItems.Add(expenseForm.ExpenseItem);
                    Session["_expenseReport"] = expenseReport;
                }

                expenseForm.ExpenseReport = expenseReport;
                expenseForm.ExpenseItem = new ExpenseItem();
                ModelState.Clear();

                return View("CreateExpense", expenseForm);
            }
            else
            {
                return PartialView(expenseForm);
            }

        }


        /// <summary>
        /// Inserts the expense report and items to the database
        /// </summary>
        /// <returns>Index in Home controller</returns>
        public ActionResult SubmitExpense()
        {
            if (Session["_expenseReport"] != null)
            {
                reportService.CreateExpenseReport((ExpenseReport)Session["_expenseReport"]);

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewMyExpenses()
        {
            return View();
        }

        /// <summary>
        /// Filters the list of expense reports by status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewMyExpenses(string status)
        {
            return View();
        }


    }
}
