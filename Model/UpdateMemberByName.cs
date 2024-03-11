using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class UpdateMemberByNameRequest
    {
        [Required]
        public string Name { get; set; }
    }
    public class UpdateMemberByNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
