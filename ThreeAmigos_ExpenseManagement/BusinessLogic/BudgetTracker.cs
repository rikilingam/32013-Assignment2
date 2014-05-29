using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class BudgetTracker : IBudgetTracker
    {
        private decimal? budgetAmount;
        private decimal? totalExpenseAmount;
        private decimal? totalExpenseProcessedCompany;
        BudgetTrackerDAL budgetTrackerDAL;

        public BudgetTracker()
        {
            budgetTrackerDAL = new BudgetTrackerDAL();
            budgetAmount = 0;
            totalExpenseAmount = 0;
            totalExpenseProcessedCompany = 0;
        }

        public BudgetTracker(Department department)
        {
            budgetTrackerDAL = new BudgetTrackerDAL();
            budgetAmount = department.MonthlyBudget;
            totalExpenseAmount = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
        }

        //public BudgetTracker(decimal companyBudget)
        //{
        //    budgetAmount = companyBudget;
        //    totalExpenseProcessedCompany = budgetTrackerDAL.TotalExpenseProcessByCompany(month, year);
        //}

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

        public decimal? CompanyBudget(int month, int year)
        {
            budgetAmount = 30000;
            totalExpenseProcessedCompany = budgetTrackerDAL.TotalExpenseProcessByCompany(month, year);
            return totalExpenseProcessedCompany;
        }

        public bool ExceedCompanyBudget(decimal? amount)
        {
            if ((budgetAmount - (totalExpenseProcessedCompany + amount)) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            return budgetTrackerDAL.CheckReportTotal(expenseId);
        }
    }
}