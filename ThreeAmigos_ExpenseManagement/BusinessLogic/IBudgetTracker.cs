using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public interface IBudgetTracker
    {
        decimal? DepartmentBudget(decimal? deptBudget, int? deptId);
        decimal? CheckReportTotal(int? expenseId);
        bool IsBudgetExceeded(decimal? amount);
    }
}