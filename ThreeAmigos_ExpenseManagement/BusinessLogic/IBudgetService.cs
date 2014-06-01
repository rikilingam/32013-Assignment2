using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public interface IBudgetService
    {
        Budget Budget { get; set; }
        void SetBudgetSpent(int month, int year);
        //decimal? CheckReportTotal(int? expenseId);
        bool IsBudgetExceeded(decimal? amount);
        //decimal? TotalExpenseAmount { get; set; }
        //decimal? RemainingAmount { get; }
        //decimal RemainingAmount(int month, int year);
        //decimal? BudgetAmount { get; }
        //decimal? CompanyBudgetRemain(int month, int year);
        //decimal? CompanyMonthlyBudget { get; }
        decimal? GetDepartmentBudgetRemain(int month, int year, Department department);
    }
}