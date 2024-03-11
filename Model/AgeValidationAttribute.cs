using System;
using System.ComponentModel.DataAnnotations;

namespace MemRegPortal.Model
{
    public class AgeValidationAttribute : ValidationAttribute
    //in this class,we check if provided dob is valid datetime. if it is we calculate age and check if its less than 18,if it is we return 'ValidationResult' with error msg
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dob)
            {
                int age = CalculateAge(dob);
                if (age < 18)
                {
                    return new ValidationResult("Age must be at least 18 years.");
                }
            }
            return ValidationResult.Success;
        }

        private int CalculateAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;        //initial age estimate by sub year of birth from current year

            if (dob.Date > today.AddYears(-age))       //it checks if birthdate is greater than date that would result by subtracting calculated date from currrent(today.AddYears(-age)). This is necessary for dates which hasn't occured
            {
                age--;
            }
            return age;
        }
    }
}