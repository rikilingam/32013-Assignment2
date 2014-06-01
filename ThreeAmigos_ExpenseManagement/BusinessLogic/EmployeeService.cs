using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Gets an employee and associate department object from the Data Access layer
        /// </summary>
        /// <param name="userId">Employees asp.net userid</param>
        /// <returns>An employee object</returns>
        public Employee GetEmployee(int userId)
        {
            EmployeeDAL employeeDAL = new EmployeeDAL();

            return employeeDAL.GetEmployee(userId);
        }
    }
}