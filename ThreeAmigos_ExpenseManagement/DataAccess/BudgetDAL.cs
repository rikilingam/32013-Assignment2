﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class BudgetDAL:IBudgetDAL
    {
        public decimal? TotalExpenseAmountByDept(int? departmentID)
        {
            int month=DateTime.Now.Month;
            int year=DateTime.Now.Year;
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                decimal? total = 0;

                var reports= from expenseReports in ctx.ExpenseReports
                            where expenseReports.ApprovedDate.Value.Month==month && expenseReports.ApprovedDate.Value.Year==year && expenseReports.Department.DepartmentId==departmentID && (expenseReports.Status=="ApprovedBySupervisor"||expenseReports.Status=="ApprovedByAccounts")
                            select expenseReports;

                foreach(var expenseItems in reports)
                {
                    foreach(var totalamount in expenseItems.ExpenseItems)
                    {
                        total = total + totalamount.AudAmount;
                    }                    
                }
                return total;
              }
            
      }


        public decimal? TotalExpenseProcessByCompany(int month, int year)
        {
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                decimal? total = 0;

                var reports = from expenseReports in ctx.ExpenseReports
                              where expenseReports.ProcessedDate.Value.Month == month && expenseReports.ProcessedDate.Value.Year == year
                                    && expenseReports.Status == "ApprovedByAccounts"
                              select expenseReports;

                foreach (var expenseItems in reports)
                {
                    foreach (var totalamount in expenseItems.ExpenseItems)
                    {
                        total = total + totalamount.AudAmount;
                    }
                }
                return total;
            }
        }


        public decimal? TotalExpenseProcessByDepartment(int month, int year, Department department)
        {
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                decimal? total = 0;

                var reports = from expenseReports in ctx.ExpenseReports 
                              where expenseReports.ApprovedDate.Value.Month == month && expenseReports.ApprovedDate.Value.Year == year
                                    && expenseReports.ApprovedById != null && expenseReports.Status=="ApprovedByAccounts"
                              select expenseReports;

                foreach (var expenseItems in reports)
                {
                    foreach (var totalamount in expenseItems.ExpenseItems)
                    {
                        total = total + totalamount.AudAmount;
                    }
                }
                return total;
            }
        }

        public decimal? GetDepartmentMonthlySpendCommitted(int month, int year, Department department)
        {
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                decimal? total = 0;

                var reports = from expenseReports in ctx.ExpenseReports
                              where expenseReports.ProcessedDate.Value.Month == month && expenseReports.ProcessedDate.Value.Year == year
                                    && expenseReports.ProcessedById != null && department.DepartmentId == expenseReports.Department.DepartmentId
                                    && expenseReports.Status == "ApprovedByAccounts"  // John June 3
                              select expenseReports;

                foreach (var expenseItems in reports)
                {
                    foreach (var totalamount in expenseItems.ExpenseItems)
                    {
                        total = total + totalamount.AudAmount;
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
                    reportTotal = reportTotal + ExpenseItem.AudAmount;
                }
               return reportTotal;
            }
        }

    }
}