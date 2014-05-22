﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.ViewModels;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class ExpenseReportService : IExpenseReportService
    {
        ExpenseReportDAL reportDAL = new ExpenseReportDAL();

        public ExpenseReport GetExpenseReport(int expenseId)
        {
            return reportDAL.GetExpenseReportById(expenseId);
        }

        public void CreateExpenseReport(ExpenseReport report)
        {
            reportDAL.InsertExpenseReport(report);
        }


        public List<ExpenseReport> GetReportsBySupervisor(string status)
        {
            return reportDAL.GetReportsBySupervisor(status);
        }

        public List<ExpenseReport> GetReportsByConsultant(string status, Employee consultant)
        {
            return reportDAL.GetExpenseReportByConsultant(status,consultant);
        }


    }
}