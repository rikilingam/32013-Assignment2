using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public interface IBudgetDAL
    {
        decimal? TotalExpenseAmountByDept(int? departmentID);
        decimal? TotalExpenseProcessByCompany(int month, int year);
        decimal? TotalExpenseProcessByDepartment(int month, int year, Department department);
        decimal? GetDepartmentMonthlySpendCommitted(int month, int year, Department department);
        decimal? CheckReportTotal(int? expenseId);
    }
}