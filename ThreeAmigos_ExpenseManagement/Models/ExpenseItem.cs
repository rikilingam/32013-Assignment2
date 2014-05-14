//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThreeAmigos_ExpenseManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class ExpenseItem
    {
        public int ItemId { get; set; }
        public int ExpenseId { get; set; }
                
        public Nullable<System.DateTime> ExpenseDate { get; set; }

        
        public string Location { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public Nullable<decimal> Amount { get; set; }
        
        public string Currency { get; set; }
        
        public Nullable<decimal> AudAmount { get; set; }
        
        public string ReceiptFileName { get; set; }
    
        public virtual ExpenseReport ExpenseReport { get; set; }
    }
}
