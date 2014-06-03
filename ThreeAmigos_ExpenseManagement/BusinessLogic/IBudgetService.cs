using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public interface IBudgetService
    {
        Budget Budget { get; set; }
        void SetBudgetSpent(int month, int year);
        bool IsBudgetExceeded(decimal? amount);        
        decimal? GetDepartmentBudgetRemain(int month, int year, Department department);
    }
}