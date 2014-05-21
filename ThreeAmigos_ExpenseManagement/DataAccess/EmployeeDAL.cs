using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class EmployeeDAL
    {
        public Employee GetEmployee(int userId)
        {
            using (EMEntitiesContext ctx = new EMEntitiesContext())
            {
                var query = from employee in ctx.Employees.Include("Department")
                            where employee.UserId == userId
                            select employee;

                return (Employee)query.First();

            }
        }
    }
}