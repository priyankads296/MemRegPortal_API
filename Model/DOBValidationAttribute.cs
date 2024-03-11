using System;
using System.ComponentModel.DataAnnotations;

namespace MemRegPortal.Model
{
    public class DOBValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dob)
            {
                //check if dob is not less than current system date
                return dob <= DateTime.Now;
            }
            return false;       //for non-DateTime values
        }
    }

}