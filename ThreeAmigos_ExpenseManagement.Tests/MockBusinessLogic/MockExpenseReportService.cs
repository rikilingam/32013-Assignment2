using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.DataAccess;

namespace ThreeAmigos_ExpenseManagement.Tests.MockBusinessLogic
{
    public class MockExpenseReportService: IExpenseReportService
    {
        ExpenseReportDAL reportDAL = new ExpenseReportDAL();
        public void CreateExpenseReport(ExpenseReport report)
        { }

        public ExpenseReport GetExpenseReport(int expenseId)
        {
            return null;
        }
        public void ActionOnReport(int? itemid, string action)
        {
           
        }
       
        public List<ExpenseReport> GetReportsBySupervisor(string status)
        {
        //    return reportDAL.GetReportsBySupervisor(status);
            //List<ExpenseReport> expReport = new List<ExpenseReport>();
            //expReport.Add(new ExpenseReport
            // {
            //     ExpenseId = 1,
            //     CreateDate = Convert.ToDateTime("29-May-14 10:23:30 PM"),
            //     CreatedById = 1,
            //     Status = ReportStatus.Submitted.ToString(),
            //     ExpenseToDept = 1
            // });
            //expReport.Add(new ExpenseReport
            //{
            //    ExpenseId = 2,
            //    CreateDate = Convert.ToDateTime("29-May-14 10:23:30 PM"),
            //    CreatedById = 1,
            //    Status = ReportStatus.Submitted.ToString(),
            //    ExpenseToDept = 1
            //});
            //expReport.Add(new ExpenseReport
            //{
            //    ExpenseId = 3,
            //    CreateDate = Convert.ToDateTime("29-May-14 10:23:30 PM"),
            //    CreatedById = 2,
            //    Status = ReportStatus.Submitted.ToString(),
            //    ExpenseToDept = 1
            //});

            //return expReport;
            return null;

              
               
        }
        public List<ExpenseReport> GetReportsByConsultant(string status, Employee consultant)
        {
            return null;
        }

        public List<ExpenseReport> GetReportsByAccounts(string status)
        {
            return null;
        }


        public void ProcessReport(int? itemid, string action)
        { }

        public List<AmountProcessedSupervisor> GetAmountSupervisor()
        {
            return null;
        }
    }
}
