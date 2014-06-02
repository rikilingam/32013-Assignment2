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

        public Budget()
        {
            budgetAmount = 0;
            Spent = 0;
        }

        public Budget(decimal? budgetAmount)
        {
            this.budgetAmount = budgetAmount;
        }
        
        /// <summary>
        /// The available budget amount for a month
        /// </summary>
        /// <param name="budgetAmount">budget value</param>
        public decimal? BudgetAmount
        {
            get
            {
                return budgetAmount;
            }
        }

        /// <summary>
        /// Returns remaining remaining amount
        /// </summary>
        public decimal? RemainingBudget
        {
            get
            {
                return budgetAmount - Spent;
            }
        }
    }
}