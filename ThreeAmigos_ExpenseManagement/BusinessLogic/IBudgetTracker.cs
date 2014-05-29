using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public interface IBudgetTracker
    {
        decimal? CheckReportTotal(int? expenseId);
        bool IsBudgetExceeded(decimal? amount);
        decimal? TotalExpenseAmount { get; set; }
        decimal? RemainingAmount { get; }
    }
}