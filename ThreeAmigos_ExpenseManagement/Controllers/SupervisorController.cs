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
        private IEmployeeService employeeService;
        private Employee employee;
        BudgetTracker bud;
        int month = DateTime.Now.Month;

        public SupervisorController()
        {
            reportService = new ExpenseReportService();
            employeeService = new EmployeeService();
            bud = new BudgetTracker();
            employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ApproveExpenses()
        {
            //decimal? reportTotal = 0;
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();

            expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("Submitted", month);
            return View(expenseForm);
        }
        
        public ActionResult ApproveExpense(int? itemid, string act)
        {
            decimal? reportTotal = 0;
            ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();

            //if (itemid == null)
            //{
            //    expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("Submitted", month);
            //    return View(expenseForm);
            //}

            //else
            //{
                if (act == "Approve")
                {
                    Budget budget = new Budget();
                    budget.totalAmountSpent = bud.DepartmentBudget(employee.Department.MonthlyBudget, employee.DepartmentId);
                    budget.totalAmountRemaining = bud.RemainingAmount;



                    reportTotal = bud.CheckReportTotal(itemid);
                    if (bud.IsBudgetExceeded(reportTotal))
                    {
                        return View("ApproveExpenses",expenseForm);
                    }

                    else
                    {
                        reportService.ActionOnReport(itemid, act);
                        expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("Submitted", month);
                        return View("ApproveExpenses", expenseForm);
                    }
                }
                else
                {
                    reportService.ActionOnReport(itemid, act);
                    expenseForm.ExpenseReports = reportService.GetReportsBySupervisor("Submitted", month);
                    return View("ApproveExpenses", expenseForm);
                }
            //}
        }

        public ActionResult ViewReports()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ViewReports(string status)
        {
          //  ExpenseFormViewModel expenseForm = new ExpenseFormViewModel();
            List<ExpenseReport> expReports =new List<ExpenseReport>();
            expReports = reportService.GetReportsBySupervisor(status, month);
            return View(expReports);
        }


        public ActionResult CheckBalance()
        {
            Budget budget = new Budget();
            budget.totalAmountSpent = bud.DepartmentBudget(employee.Department.MonthlyBudget, employee.DepartmentId);
            budget.totalAmountRemaining = bud.RemainingAmount;
            return View(budget);

        }
    }
}
