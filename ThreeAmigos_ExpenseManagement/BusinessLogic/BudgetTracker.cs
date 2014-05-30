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
        private decimal? companyMonthlyBudget; 
        BudgetTrackerDAL budgetTrackerDAL;

        public BudgetTracker()
        {
            budgetTrackerDAL = new BudgetTrackerDAL();
            budgetAmount = 0;
            totalExpenseAmount = 0;
            totalExpenseProcessedCompany = 0;
            companyMonthlyBudget = CurrencyService.GetCompanyMonthlyBudget(); 
        }

        public BudgetTracker(Department department)
        {
            budgetTrackerDAL = new BudgetTrackerDAL();
            budgetAmount = department.MonthlyBudget;
            companyMonthlyBudget = CurrencyService.GetCompanyMonthlyBudget(); 
            totalExpenseAmount = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
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

        public decimal? CompanyBudgetRemain(int month, int year)
        {
            totalExpenseProcessedCompany = budgetTrackerDAL.TotalExpenseProcessByCompany(month, year);
            return (companyMonthlyBudget - totalExpenseProcessedCompany);
        }


        public decimal? GetDepartmentBudgetRemain(int month, int year, Department department)
        {
            decimal? totalExpenseProcessedDepartment = 0;
            budgetAmount = department.MonthlyBudget;
            totalExpenseProcessedDepartment = budgetTrackerDAL.TotalExpenseProcessByDepartment(month, year, department);
            return (budgetAmount - totalExpenseProcessedDepartment);
        }
        

        public bool ExceedCompanyBudget(int month, int year, decimal? amount)
        {
            if (amount > CompanyBudgetRemain(month, year) )
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