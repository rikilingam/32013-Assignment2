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
        //
        // GET: /Supervisor/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ApproveExpenses()
        {
            Employee employee = IntializeEmployee((int)Membership.GetUser().ProviderUserKey);
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00 NEED TO CORRECT
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

           
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var result = (from i in ctx.ExpenseReports.Include("CreatedBy").Include("ExpenseItems").Include("Department")
                              where i.Department.DepartmentId == employee.Department.DepartmentId && i.CreateDate>=startDateTime &&i.CreateDate<=endDateTime
                              select i);
                return View(result.ToList());
            }
        }

        private Employee IntializeEmployee(int userId)
        {

            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var query = from employee in ctx.Employees.Include("Department")
                            where employee.UserId == userId
                            select employee;

                return (Employee)query.First();

            }

        }
    }
}
