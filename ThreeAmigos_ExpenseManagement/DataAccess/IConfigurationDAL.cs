using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    /// <summary>
    /// This interface is used to define methods to retrieve application settings from web.config file
    /// </summary>
    public interface IConfigurationDAL
    {
        object GetAppSetting(string key);
    }
}