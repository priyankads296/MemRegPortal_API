using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class GetAllRecordResponse 
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<InsertMemberRequest> data { get; set; }
    }
}
