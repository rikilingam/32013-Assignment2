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
    
    public partial class Employee
    {
        public Employee()
        {
            this.ApprovedBy = new HashSet<ExpenseReport>();
            this.ProcessedBy = new HashSet<ExpenseReport>();
            this.CreatedBy = new HashSet<ExpenseReport>();
        }
    
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<ExpenseReport> ApprovedBy { get; set; }
        public virtual ICollection<ExpenseReport> ProcessedBy { get; set; }
        public virtual ICollection<ExpenseReport> CreatedBy { get; set; }
    }
}
