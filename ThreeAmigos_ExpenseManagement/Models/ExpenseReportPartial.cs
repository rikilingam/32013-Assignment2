using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.BusinessLogic;

namespace ThreeAmigos_ExpenseManagement.Models
{
    public enum ReportStatus { Submitted, RejectedBySupervisor, ApprovedBySupervisor, RejectedByAccounts, ApprovedByAccounts }

    public partial class ExpenseReportPartial
    {
        public decimal ExpenseTotal { get; set; }
    }

}

