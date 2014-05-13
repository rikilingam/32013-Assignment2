using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.Models;
using System.ComponentModel.DataAnnotations;
using ThreeAmigos_ExpenseManagement.BusinessLogic;

namespace ThreeAmigos_ExpenseManagement.ViewModels
{
    //View Model for create expense
    public class ExpenseFormViewModel
    {
        [Display(Name = "Employee Name")]
        public string employeeName { get; set; }

        [Display(Name = "Department")]
        public string departmentName { get; set; }

        public ExpenseReport expenseReport { get; set; }

        public ExpenseItem expenseItem { get; set; }


        public int noOfItems { get; set; }


    }
}