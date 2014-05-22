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
    public class SupervisorController : Controller
    {
        IExpenseReportService reportService = new ExpenseReportService();
        //
        // GET: /Supervisor/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ApproveExpenses()
        {
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("Submitted");
            return View(expenseForm);

        }

        [HttpPost]
        public ActionResult ViewReports(string status)
        {
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            expenseForm.ExpenseReports = reportService.GetReportsBySupervisor(status);
            return View(expenseForm);
        }
        
        public ActionResult ViewReports()
        {
            return View();
        }

    }
}
