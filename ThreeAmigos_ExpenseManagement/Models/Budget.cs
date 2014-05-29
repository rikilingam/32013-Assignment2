using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.Models
{
    public class Budget
    {
        public decimal? totalAmountSpent { get; set; }
        public decimal? totalAmountRemaining { get; set; }
        public decimal? companyAmountSpent { get; set; }
        public decimal? companyAmountRemaining { get; set; }
        public Budget()
        {
            totalAmountRemaining = 2000; // John
            companyAmountRemaining = 14000;  // John
        }
    }
}