using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.ViewModels
{
    public class BudgetViewModel
    {
        public decimal? totalAmountSpent { get; set; }
        public decimal? totalAmountRemaining { get; set; }
    }
}