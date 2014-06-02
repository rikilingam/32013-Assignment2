using System;
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

        /// <summary>
        /// Get a expense report from the ReportDAL
        /// </summary>
        /// <param name="expenseId">Unique expense id</param>
        /// <returns></returns>
        public ExpenseReport GetExpenseReport(int expenseId)
        {
            return reportDAL.GetExpenseReportById(expenseId);
        }

        /// <summary>
        /// Get a list of expense reports from the ReportDAL
        /// </summary>
        /// <param name="status">status of the report</param>
        /// <param name="consultant">consultant</param>
        /// <returns></returns>
        public List<ExpenseReport> GetReportsByConsultant(string status, Employee consultant)
        {

            return reportDAL.GetExpenseReportByConsultant(status, consultant);

        }

        /// <summary>
        /// Inserts a new Expense report into the ReportDAL
        /// </summary>
        /// <param name="report">expense report object</param>
        public void CreateExpenseReport(ExpenseReport report)
        {
            reportDAL.InsertExpenseReport(report);
        }

        /// <summary>
        /// Gets a list of expense reports from the DAL for a supervisors department
        /// </summary>
        /// <param name="status">status of the report</param>
        /// <returns>list of reports</returns>
        public List<ExpenseReport> GetReportsBySupervisor(string status)
        {
            return reportDAL.GetReportsBySupervisor(status);
        }

        /// <summary>
        /// Get a list of expense reports for by status for Accounts
        /// </summary>
        /// <param name="status">status of reports</param>
        /// <returns>List of reports</returns>
        public List<ExpenseReport> GetReportsByAccounts(string status)
        {
            return reportDAL.GetReportsByAccounts(status);
        }

        /// <summary>
        /// Get amount processed by supervisor
        /// </summary>
        /// <returns>list of amounts processed</returns>
        public List<AmountProcessedSupervisor> GetAmountSupervisor()
        {
            return reportDAL.GetAmountSupervisor();
        }

        /// <summary>
        /// Approve or Declines a report for Supervisor
        /// </summary>
        /// <param name="expenseId">Expense Id of the report</param>
        /// <param name="employee">Supervisor</param>
        /// <param name="status">Approve or Decline</param>
        public void ActionOnReport(int? expenseId,Employee employee,ReportStatus status)
        {
            reportDAL.ActionOnReport(expenseId, employee, status);
        }

        /// <summary>
        /// Approve or Declines a report for Accounts
        /// </summary>
        /// <param name="expenseId">Expense Id of the report</param>
        /// <param name="employee">Accounts person</param>
        /// <param name="status">Approve or Decline</param>
        public void ProcessReport(int? expenseId, Employee employee, ReportStatus status)
        {
            reportDAL.ProcessReport(expenseId, employee, status);
        }
    }
}