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
    [Authorize]
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
            Employee employee = new Employee();
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            List<ExpenseItem> items = new List<ExpenseItem>();
            ControllerContext.HttpContext.Session["_expenseItems"] = items;
            employee = IntializeEmployee();

            expenseForm.expenseItem = new ExpenseItem();
            expenseForm.expenseReport = new ExpenseReport();
            
            expenseForm.employeeName = employee.Firstname + " " + employee.Surname;
            expenseForm.departmentName = employee.Department.DepartmentName;
            expenseForm.expenseReport.CreateDate = DateTime.Now;
            expenseForm.noOfItems = 0;
            
            return View(expenseForm);

        }

        [HttpPost]
        public ActionResult CreateExpense(ExpenseFormViewModel expenseForm)
        {
            List<ExpenseItem> items = new List<ExpenseItem>();

            expenseForm.expenseItem.AudAmount = CurrencyService.CalcAud(expenseForm.expenseItem.Amount, expenseForm.expenseItem.Currency);

            if (ControllerContext.HttpContext.Session["_expenseItems"] != null)
            {
                items = (List<ExpenseItem>)ControllerContext.HttpContext.Session["_expenseItems"];
                items.Add(expenseForm.expenseItem);
                ControllerContext.HttpContext.Session["_expenseItems"] = items;
            }
            else
            {
                items.Add(expenseForm.expenseItem);
                ControllerContext.HttpContext.Session["_expenseItems"] = items;
            }

            expenseForm.expenseReport.ExpenseItems = items;

            expenseForm.expenseItem = new ExpenseItem();
            
            ModelState.Clear();
            return View("CreateExpense", expenseForm);

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

                foreach (Employee employee in query)
                {
                    emp = employee;

                }

            }
            return emp;

        }

    }
}
