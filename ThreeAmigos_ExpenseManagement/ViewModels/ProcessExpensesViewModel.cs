using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.ViewModels
{
    public class ProcessExpensesViewModel
    {
        public List<ExpenseReport> ExpenseReports { get; set; }
        public IBudgetService BudgetTracker { get; set; }
    }
}