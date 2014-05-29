using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThreeAmigos_ExpenseManagement.BusinessLogic;

namespace ThreeAmigos_ExpenseManagement.Models
{
    [MetadataType(typeof(ExpenseItemMetaData))]
    public partial class ExpenseItem
    {
    }

    public partial class ExpenseItemMetaData
    {
        [Required]
        [Display(Name = "Expense Date")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [MaximumDate]
        public Nullable<System.DateTime> ExpenseDate { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public Nullable<decimal> Amount { get; set; }
    } 
}