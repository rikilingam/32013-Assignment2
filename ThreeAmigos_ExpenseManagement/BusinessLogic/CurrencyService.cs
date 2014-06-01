using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class CurrencyService
    {
        IConfigurationDAL config;

        public CurrencyService()
        {
            config = new ConfigurationDAL();
        }

        public CurrencyService(IConfigurationDAL config)
        {
            this.config = config;
        }

        /// <summary>
        /// Get the currency rate from the web.config for a given currency
        /// </summary>
        /// <param name="currency">The currency short code</param>
        /// <returns>returns the rate</returns>
        private decimal GetRate(string currency)
        {
            decimal rate = 0;
                        
            if (decimal.TryParse(config.GetAppSetting(currency) as string, out rate) && rate > 0)
            {
                return rate;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Calculate AUD value of a given amount in non-AUD currency
        /// </summary>
        /// <param name="amount">Original amount</param>
        /// <param name="currency">The currency code</param>
        /// <returns>AUD value</returns>
        public decimal? CalcAud(decimal? amount, string currency)
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
    }
}