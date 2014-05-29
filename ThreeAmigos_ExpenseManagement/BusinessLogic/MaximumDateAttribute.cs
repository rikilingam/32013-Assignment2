using System;
using System.ComponentModel.DataAnnotations;

namespace ThreeAmigos_ExpenseManagement.BusinessLogic
{
    public class MaximumDateAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);

            if (dateTime <= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}