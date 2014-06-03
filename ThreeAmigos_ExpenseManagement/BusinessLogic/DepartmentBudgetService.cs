using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class DepartmentBudgetService : IBudgetService
    {
        Department department;        
        IBudgetDAL budgetTrackerDAL;

        public Budget Budget { get; set; }

        public DepartmentBudgetService(Department department)
        {
            this.department = department;
            Budget = new Budget(department.MonthlyBudget);
            budgetTrackerDAL = new BudgetDAL();
        }

        public DepartmentBudgetService(Department department, IBudgetDAL budgetDal)
        {
            this.department = department;
            Budget = new Budget(department.MonthlyBudget);
            budgetTrackerDAL = budgetDal;
        }

        /// <summary>
        /// Calculate the budget remaining
        /// </summary>
        /// <param name="month">month of the year</param>
        /// <param name="year">year</param>
        /// <returns>Remaining Amount</returns>
        public decimal RemainingAmount(int month, int year)
        {
            Budget.Spent = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
            return Budget.RemainingBudget ?? 0;
        }

        /// <summary>
        /// Calculate the budget spent
        /// </summary>
        /// <param name="month">month of the year</param>
        /// <param name="year">year</param>
        /// <returns>Amount spent </returns>
        public void SetBudgetSpent(int month, int year)
        {
            Budget.Spent = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
        }

        /// <summary>
        /// Checks whether approving the report will lead to department going over budget
        /// </summary>
        /// <param name="amount">amount</param>
        /// <returns>boolean result whether budget exceed or not</returns>
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

        public decimal? GetDepartmentBudgetRemain(int month, int year, Department department)
        {
            Budget deptBudget = new Budget(department.MonthlyBudget);
            deptBudget.Spent = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
            return (deptBudget.RemainingBudget);
        }
    }
}