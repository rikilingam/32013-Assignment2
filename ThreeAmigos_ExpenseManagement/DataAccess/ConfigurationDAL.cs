using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class ConfigurationDAL:IConfigurationDAL
    {
        public object GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        
    }
}