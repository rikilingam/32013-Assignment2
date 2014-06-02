using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.DataAccess;

namespace ThreeAmigos_ExpenseManagement.Tests.Mocks
{
    /// <summary>
    /// Mock ConfigurationDAL which gets a value from the web.config with a given key
    /// </summary>
    class MockConfigurationDAL:IConfigurationDAL
    {
        public object GetAppSetting(string key)
        {
            if (key == "CompanyMonthlyBudget") // return the company budget value
            {
                return "30000";
            }
            else if (key == "EUR") //return EUR exchange rate
            {
                return "1.49146";
            }
            else if (key == "CNY") //return CNY exchange rate
            {
                return "0.17430";
            }
            else if (key == "ReceiptItemFilePath") //return receipt repository path
            {
                return "/Receipts/";
            }
            else
            {
                return null;
            }
        }
     
    }
}
