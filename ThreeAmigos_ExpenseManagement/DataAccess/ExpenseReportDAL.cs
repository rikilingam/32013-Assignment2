﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.BusinessLogic;

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


        public List<ExpenseReport> GetReportsBySupervisor(string status)
        {
     
            IEmployeeService employeeService = new EmployeeService();
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00 NEED TO CORRECT
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);
            Employee employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var result = (from i in ctx.ExpenseReports.Include("CreatedBy").Include("ExpenseItems").Include("Department")
                              where i.Department.DepartmentId == employee.Department.DepartmentId && i.CreateDate >= startDateTime && i.CreateDate <= endDateTime && i.Status==status
                              select i);
                return (List<ExpenseReport>)result.ToList();              
            }         
        }


    }
}