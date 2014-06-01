using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;


namespace ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic
{
    class MockBudgetService:IBudgetService
    {
        Department department; 
        public MockBudgetService(Department department)
        {
             this.department = department;
             Budget = new Budget(department.MonthlyBudget);
            }

        public Budget Budget
        {
            get
             {
              return Budget =new Budget(department.MonthlyBudget);    
             }
            set
            {
                Budget budget = new Budget(department.MonthlyBudget);
                budget.Spent = 4000;
            }
        }

        public void SetBudgetSpent(int month, int year)
        {
        }

        public bool IsBudgetExceeded(decimal? amount)
        {
            throw new NotImplementedException();
        }

        public decimal? GetDepartmentBudgetRemain(int month, int year, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
