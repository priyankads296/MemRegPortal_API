using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class DependentRequest
    {
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DOBValidation(ErrorMessage = "DOB should not be less than the current date.")]         //custom validation
        [AgeValidation(ErrorMessage = "Age must be at least 18 years.")]                        //custom validation
        public DateTime DOB { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10})$")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9]{12}$")]
        public string PAN { get; set; }
    }
    public class DependentResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
