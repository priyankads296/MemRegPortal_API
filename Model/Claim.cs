using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class ClaimRequest
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.String)]
        //public string? Id { get; set; }

        public string Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string MemberName { get; set; }
        [Required]
        [DOBValidation(ErrorMessage = "DOB should not be less than the current date.")]
        [ReadOnly(true)]
        public DateTime DOB { get; set; }
        [Required]
        [AdmissionDateValidation(ErrorMessage = "Date of admission should be on or before date of discharge.")]
        public DateTime AdmissionDate { get; set; }
        [Required]
        public DateTime DischargeDate { get; set; }
        [Required]
        public string ProviderName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        
        public List<DependentRequest> Dependents { get; set; }
        

    }

    public class ClaimResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }

    public class AdmissionDateValidationAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var claim = (ClaimRequest)validationContext.ObjectInstance;
            if(claim.AdmissionDate<=claim.DischargeDate)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Date Of Admission must be on or before the Date Of Discharge.");
        }
    }
   
}
