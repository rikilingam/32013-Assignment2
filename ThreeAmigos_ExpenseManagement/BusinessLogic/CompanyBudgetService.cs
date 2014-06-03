using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class CompanyBudgetService : IBudgetService
    {
        IBudgetDAL budgetTrackerDAL;

        public Budget Budget { get; set; }
        
        public CompanyBudgetService(decimal budgetAmount)
        {
            budgetTrackerDAL = new BudgetDAL();
            Budget = new Budget(budgetAmount);
        }

        public CompanyBudgetService(decimal budgetAmount, IBudgetDAL budgetDAL)
        {
            budgetTrackerDAL = budgetDAL;
            Budget = new Budget(budgetAmount);
        }

      
        public static decimal GetCompanyMonthlyBudget()
        {
            decimal budget = 0;

            if (decimal.TryParse(ConfigurationManager.AppSettings["CompanyMonthlyBudget"], out budget) && budget > 0)
            {
                return budget;
            }
            else
            {
                return 0;
            }
        }


        public void SetBudgetSpent(int month, int year)
        {
            Budget.Spent = budgetTrackerDAL.TotalExpenseProcessByCompany(month, year);
        }

        public decimal? GetDepartmentBudgetRemain(int month, int year, Department department)
        {
            Budget deptBudget = new Budget(department.MonthlyBudget);
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