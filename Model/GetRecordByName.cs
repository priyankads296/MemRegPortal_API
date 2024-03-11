using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class GetRecordByNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public InsertMemberRequest data { get; set; }
    }
    public class GetRecordByNameRequest
    {
        public string name { get; set; }
    }

}
