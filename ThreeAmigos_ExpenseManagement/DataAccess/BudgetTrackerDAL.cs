﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class BudgetTrackerDAL
    {
        public decimal? TotalExpenseAmountByDept(int? departmentID)
        {
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                decimal? total = 0;

                var reports= from expenseReports in ctx.ExpenseReports
                            where expenseReports.Department.DepartmentId==departmentID && (expenseReports.Status=="ApprovedBySupervisor"||expenseReports.Status=="ApprovedByAccounts")
                            select expenseReports;

                foreach(var expenseItems in reports)
                {
                    foreach(var totalamount in expenseItems.ExpenseItems)
                    {
                         total = total + totalamount.Amount;
                    }                    
                }
                return total;
              }
            
      }

        public decimal? CheckReportTotal(int? expenseId)
        {
            decimal? reportTotal = 0;
            using(EMEntitiesContext ctx=new EMEntitiesContext())
            {
                var report=(from reports in ctx.ExpenseReports.Include("ExpenseItems")
                           where reports.ExpenseId==expenseId
                           select reports).Single();

                foreach (var ExpenseItem in report.ExpenseItems)
                {
                    reportTotal = reportTotal + ExpenseItem.Amount;
                }
               return reportTotal;
            }
        }

    }
}