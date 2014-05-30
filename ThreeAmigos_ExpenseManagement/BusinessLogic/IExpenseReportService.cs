using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public interface IExpenseReportService
    {
        void CreateExpenseReport(ExpenseReport report);
        
        ExpenseReport GetExpenseReport(int expenseId);
        List<ExpenseReport> GetReportsBySupervisor(string status);
        void ActionOnReport(int? itemid, string action);
        void ProcessReport(int? itemid, string action);

        List<ExpenseReport> GetReportsByConsultant(string status, Employee consultant);
    }
}