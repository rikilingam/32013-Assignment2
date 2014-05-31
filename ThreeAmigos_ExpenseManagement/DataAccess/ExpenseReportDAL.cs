using System;
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

        public List<ExpenseReport> GetExpenseReportByConsultant(string status, Employee consultant)
        {

            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var result = from report in ctx.ExpenseReports.Include("CreatedBy").Include("ExpenseItems").Include("Department")
                             where report.CreatedBy.UserId == consultant.UserId && report.Status == status
                             select report;

                return (List<ExpenseReport>)result.ToList();
            }
        }

        public List<ExpenseReport> GetReportsBySupervisor(string status)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            IEmployeeService employeeService = new EmployeeService();
            Employee employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var result = (from i in ctx.ExpenseReports.Include("CreatedBy").Include("ExpenseItems").Include("Department")
                              where i.Department.DepartmentId == employee.Department.DepartmentId && i.CreateDate.Value.Month == month &&i.CreateDate.Value.Year==year && i.Status == status
                              select i);

                return (List<ExpenseReport>)result.ToList();
            }
        }

        public List<ExpenseReport> GetReportsByAccounts(string status)
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var result = (from report in ctx.ExpenseReports.Include("CreatedBy").Include("ExpenseItems").Include("Department").Include("ApprovedBy")
                              where report.CreateDate.Value.Month == month && report.CreateDate.Value.Year == year && report.Status == status
                              select report);

                return (List<ExpenseReport>)result.ToList();
            }
        }


        public List<AmountProcessedSupervisor> GetAmountSupervisor()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var totalExpenseInOneReport = from expense in ctx.ExpenseItems
                                              group expense by expense.ExpenseId into exp
                                              select new
                                              {
                                                  expID = exp.Key,
                                                  totalAmount = exp.Sum(expense => expense.AudAmount)
                                              };

                var reportsApprovedInCurrentMonth = from report in ctx.ExpenseReports
                                                    join ex in totalExpenseInOneReport
                                                    on report.ExpenseId equals ex.expID
                                                    where report.CreateDate.Value.Month == month && report.CreateDate.Value.Year == year && report.Status == "ApprovedByAccounts"
                                                    select new
                                                    {
                                                        supervisorID = report.ApprovedById,
                                                        total = ex.totalAmount
                                                    };

                var totalExpenseApprovedBySupervisor = from report in reportsApprovedInCurrentMonth
                                                       group report by report.supervisorID into rep
                                                       select new
                                                       {
                                                           supervisorID = rep.Key,
                                                           total = rep.Sum(report => report.total)
                                                       };

                var result = from emp in ctx.Employees
                                  join spent in totalExpenseApprovedBySupervisor
                                  on emp.UserId equals spent.supervisorID
                                  select new AmountProcessedSupervisor
                                  {
                                      Fullname = emp.Firstname + " " + emp.Surname,
                                      amountApproved = spent.total
                                  };
                
                return (List<AmountProcessedSupervisor>)result.ToList();
            }
        }


        public void ActionOnReport(int? itemid, string action)
        {
            IEmployeeService employeeService = new EmployeeService();
            Employee employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {

                var report = (from ExpReport in ctx.ExpenseReports
                              where ExpReport.ExpenseId == itemid
                              select ExpReport).FirstOrDefault();

                if (action == "Approve")
                {
                    report.ApprovedDate = DateTime.Now;
                    report.Status = "ApprovedBySupervisor";
                    report.ApprovedById = employee.UserId;
                    ctx.SaveChanges();
                }
                else
                {
                    report.ApprovedDate = DateTime.Now;
                    report.Status = "RejectedBySupervisor";
                    report.ApprovedById = employee.UserId;
                    ctx.SaveChanges();
                }
            }
        }

        public void ProcessReport(int? itemid, string action)
        {
            IEmployeeService employeeService = new EmployeeService();
            Employee employee = employeeService.GetEmployee((int)Membership.GetUser().ProviderUserKey);

            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {

                var report = (from ExpReport in ctx.ExpenseReports
                              where ExpReport.ExpenseId == itemid
                              select ExpReport).FirstOrDefault();

                if (action == "Approve")
                {
                    report.ProcessedDate = DateTime.Now;
                    report.Status = "ApprovedByAccounts";
                    report.ProcessedById = employee.UserId;
                    ctx.SaveChanges();
                }
                else
                {
                    report.ProcessedDate = DateTime.Now;
                    report.Status = "RejectedByAccounts";
                    report.ProcessedById = employee.UserId;
                    ctx.SaveChanges();
                }
            }
        }

    }
} 