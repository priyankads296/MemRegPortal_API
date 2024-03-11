using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class UserRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
       
        public string? UserID { get; set; }
        
        
        [Required(ErrorMessage = "FirstName is required")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(\d{10})$")]
        public string PhoneNo { get; set; }




    }
    public class UserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
