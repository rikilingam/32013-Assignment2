using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class CompanyBudgetService : IBudgetService
    {
        BudgetTrackerDAL budgetTrackerDAL;

        public Budget Budget { get; set; }
        
        public CompanyBudgetService(decimal budgetAmount)
        {
            budgetTrackerDAL = new BudgetTrackerDAL();
            Budget = new Budget(budgetAmount);
        }

        //public decimal RemainingAmount(int month, int year)
        //{
        //    Budget.Spent = budgetTrackerDAL.TotalExpenseProcessByCompany(month, year);
        //    return Budget.RemainingBudget??0;
        //}

        public void SetBudgetSpent(int month, int year)
        {
            Budget.Spent = budgetTrackerDAL.TotalExpenseProcessByCompany(month, year);
        }

        public decimal? GetDepartmentBudgetRemain(int month, int year, Department department)
        {
            Budget deptBudget = new Budget(department.MonthlyBudget);

            //decimal? totalExpenseProcessedDepartment = 0;
            //budgetAmount = department.MonthlyBudget;
            //totalExpenseProcessedDepartment = budgetTrackerDAL.TotalExpenseProcessByDepartment(month, year, department);
            deptBudget.Spent = budgetTrackerDAL.GetDepartmentMonthlySpendCommitted(month, year, department);
            return (deptBudget.RemainingBudget);
        }

        public bool IsBudgetExceeded(decimal? amount)
        {
            if ((Budget.BudgetAmount - (Budget.Spent + amount)) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}