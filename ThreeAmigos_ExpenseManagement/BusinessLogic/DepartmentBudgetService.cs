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

        public decimal RemainingAmount(int month, int year)
        {
            //  Budget.Spent = budgetTrackerDAL.TotalExpenseProcessByDepartment(month, year, department);
            Budget.Spent = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
            return Budget.RemainingBudget ?? 0;
        }

        public void SetBudgetSpent(int month, int year)
        {
            Budget.Spent = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
         //   Budget.Spent = budgetTrackerDAL.TotalExpenseProcessByDepartment(month, year, department);
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

        public decimal? GetDepartmentBudgetRemain(int month, int year, Department department)
        {
            Budget deptBudget = new Budget(department.MonthlyBudget);

            //decimal? totalExpenseProcessedDepartment = 0;
            //budgetAmount = department.MonthlyBudget;
            //totalExpenseProcessedDepartment = budgetTrackerDAL.TotalExpenseProcessByDepartment(month, year, department);
           // deptBudget.Spent = budgetTrackerDAL.TotalExpenseProcessByDepartment(month, year, department);
            deptBudget.Spent = budgetTrackerDAL.TotalExpenseAmountByDept(department.DepartmentId);
            return (deptBudget.RemainingBudget);
        }
    }
}