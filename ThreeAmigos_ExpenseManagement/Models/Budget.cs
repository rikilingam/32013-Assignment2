using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.Models
{
    public class Budget
    {
        protected decimal? budgetAmount;
        public decimal? Spent { get; set; }

        //public decimal? totalAmountSpent { get; set; }
        //public decimal? totalAmountRemaining { get; set; }
        //public decimal? companyAmountSpent { get; set; }
        //public decimal? companyAmountRemaining { get; set; }
        public Budget()
        {
            budgetAmount = 0;
            Spent = 0;
        }

        public Budget(decimal? budgetAmount)
        {
            this.budgetAmount = budgetAmount;
        }

        public decimal? BudgetAmount
        {
            get
            {
                return budgetAmount;
            }
        }

        public decimal? RemainingBudget
        {
            get
            {
                return budgetAmount - Spent;
            }
        }
    }
}