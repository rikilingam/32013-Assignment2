using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic
{
    public class MockExpenseReportService: IExpenseReportService
    {
        public void CreateExpenseReport(ExpenseReport report)
        { }

        public ExpenseReport GetExpenseReport(int expenseId)
        {
            return null;
        }
    }
}
