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
    [Authorize(Roles = "Consultant")]
    [InitializeSimpleMembership]
    public class ConsultantController : Controller
    {
        //
        // GET: /Consultant/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateExpense()
        {
            Employee employee = IntializeEmployee();
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            ExpenseReport expenseReport = new ExpenseReport();

            expenseReport.CreateDate = DateTime.Now;
            expenseReport.CreatedBy = employee;
            expenseReport.Department = employee.Department;

            Session["_expenseReport"] = expenseReport;

            expenseForm.ExpenseReport = expenseReport;
            return View(expenseForm);

        }

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


        public ActionResult SubmitExpense()
        {
            return View();
        }

        public ActionResult ViewMyExpenses()
        {
            return View();
        }


        private Employee IntializeEmployee()
        {
            int userId = (int)Membership.GetUser().ProviderUserKey;
            Employee emp = new Employee();

            using (Entities emEntities = new Entities())
            {
                var query = from employee in emEntities.Employees.Include("Department")
                            where employee.UserId == userId
                            select employee;
                emp = query.First();

            }
            return emp;

        }

    }
}
