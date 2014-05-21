using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class ExpenseReportDAL
    {
        public void InsertExpenseReport(ExpenseReport report)
        {
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                ExpenseReport newReport = new ExpenseReport();

                newReport.CreatedBy = ctx.Employees.First(e => e.UserId == report.CreatedBy.UserId);
                newReport.CreateDate = report.CreateDate;
                newReport.Department = ctx.Departments.First(d => d.DepartmentId == report.Department.DepartmentId);
                newReport.Status = ReportStatus.Submitted.ToString();
                newReport.ExpenseItems = report.ExpenseItems;

                ctx.ExpenseReports.Add(newReport);
                ctx.SaveChanges();
            }
        }


        public ExpenseReport GetExpenseReportById(int expenseId)
        {
            throw new NotImplementedException();
        }
    }
}