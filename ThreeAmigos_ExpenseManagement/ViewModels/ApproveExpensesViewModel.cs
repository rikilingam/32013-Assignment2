using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.ViewModels
{
    public class ApproveExpensesViewModel
    {
        public List<ExpenseReport> ExpenseReports { get; set; }
        public IBudgetService BudgetTracker { get; set; }

        public ApproveExpensesViewModel()
        {
            ExpenseReports = new List<ExpenseReport>();
            BudgetTracker = new BudgetTracker();
        }
    }
}