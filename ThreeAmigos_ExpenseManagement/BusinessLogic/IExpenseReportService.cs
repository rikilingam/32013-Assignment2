﻿using System;
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
        List<ExpenseReport> GetReportsByAccounts(string status);
        List<AmountProcessedSupervisor> GetAmountSupervisor();
        void ActionOnReport(int? expenseId, Employee employee, ReportStatus status);
        void ProcessReport(int? expenseId, Employee employee, ReportStatus status);

        List<ExpenseReport> GetReportsByConsultant(string status, Employee consultant);
    }
}