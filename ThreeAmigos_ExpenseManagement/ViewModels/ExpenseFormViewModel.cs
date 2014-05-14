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
        public string EmployeeName { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        [Display(Name="Create Date")]
        public DateTime CreateDate { get; set; }

        public ExpenseReport ExpenseReport { get; set; }              

        public ExpenseItem ExpenseItem { get; set; }

        public ExpenseFormViewModel()
        {
            ExpenseReport = new ExpenseReport();
        }
    }
}