using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.Tests.Mocks
{
    /// <summary>
    /// Mock BudgetDAL used to provide dummy results from database
    /// </summary>
 
    public class MockBudgetDAL : IBudgetDAL
    {
        Department department;

 
        public MockBudgetDAL()
        {
            department = new Department();
            department.DepartmentId = 1; 
            department.DepartmentName = "Test Department"; 
            department.MonthlyBudget = 10000;
        }

        public decimal? TotalExpenseAmountByDept(int? departmentID)
        {
            if (department.DepartmentId == departmentID)
            {
                return 9900;
            }
            else
            {
                return 0;
            }
        }


        public decimal? TotalExpenseProcessByCompany(int month, int year)
        {
            if (month == 6 && year == 2014)
            {
                return 25000; 
            }
            else
            {
                return 0;
            }
        }


        public decimal? TotalExpenseProcessByDepartment(int month, int year, Department department)
        {
            if (month == 1 && year == 2014 && department == this.department)
            {
                return 6500;
            }
            else
            {
                return 0;
            }            
        }

        public decimal? GetDepartmentMonthlySpendCommitted(int month, int year, Department department)
        {
            if (month == 1 && year == 2014 && department == this.department)
            {
                return 5500;
            }
            else
            {
                return 0;
            }
        }


        public decimal? CheckReportTotal(int? expenseId)
        {
            if (expenseId == 1)
            {
                return 1250;
            }
            else
            {
                return 0;
            }
        }
    }
}
