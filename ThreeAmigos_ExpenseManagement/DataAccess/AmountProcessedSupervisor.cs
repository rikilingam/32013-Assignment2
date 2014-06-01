using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.BusinessLogic;

namespace ThreeAmigos_ExpenseManagement.Models
{
    public class AmountProcessedSupervisor
    {
        public string Fullname { get; set; }
        public decimal? amountApproved { get; set; }
        public decimal? companyMonthlyBudget = CurrencyService.GetCompanyMonthlyBudget();
    }
}