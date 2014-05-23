using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class BudgetTracker:IBudgetTracker
    {
        private decimal? budgetAmount;
        private decimal? totalExpenseAmount;  
        BudgetTrackerDAL budgetTracker;

        public BudgetTracker()
        {
            budgetTracker = new BudgetTrackerDAL();
            budgetAmount = 0;
            totalExpenseAmount = 0;
        }

        public decimal? TotalExpenseAmount
        {
            get
            {
                return totalExpenseAmount;
            }

            set
            {
                if (value > 0)
                    totalExpenseAmount += value;
            }
        }

        public decimal? BudgetAmount
        {
            get { return budgetAmount; }
        }

        public decimal? DepartmentBudget(decimal? deptBudget, int? deptId)
        {
             budgetAmount = deptBudget;
             TotalExpenseAmount = budgetTracker.TotalExpenseAmountByDept(deptId);
             return TotalExpenseAmount;
        }

        public decimal? RemainingAmount
        {
            get
            {
                return budgetAmount - totalExpenseAmount;
            }
        }

        public bool IsBudgetExceeded(decimal? amount)
        {
            if ((budgetAmount - (totalExpenseAmount + amount)) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal? CheckReportTotal(int? expenseId)
        {
            return budgetTracker.CheckReportTotal(expenseId);
        }
    }
}