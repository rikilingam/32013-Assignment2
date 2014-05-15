using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.Models
{
    public partial class Employee
    {

        public string Fullname
        {
            get { return Firstname + " " + Surname; }
        }
    
    }

}