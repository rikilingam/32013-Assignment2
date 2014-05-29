using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public static class CurrencyService
    {

        private static decimal GetRate(string currency)
        {
            decimal rate = 0;

            if (decimal.TryParse(ConfigurationManager.AppSettings[currency], out rate) && rate > 0)
            {
                return rate;
            }
            else
            {
                return 0;
            }
        }

        public static decimal? CalcAud(decimal? amount, string currency)
        {                       
            decimal? audAmount = 0;

            if (currency == "AUD" && amount > 0)
            {
                audAmount = amount;
            }
            else
            {
                audAmount = amount * GetRate(currency);
            }

            return audAmount;
        }

        public static decimal GetCompanyMonthlyBudget()
        {
            decimal budget = 0;

            if (decimal.TryParse(ConfigurationManager.AppSettings["CompanyMonthlyBudget"], out budget) && budget > 0)
            {
                return budget;
            }
            else
            {
                return 0;
            }
        }
    }
}