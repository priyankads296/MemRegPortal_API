using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.Model
{
    public class GetRecordByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public InsertMemberRequest data { get; set; }
    }
    public class GetRecordByIdRequest
    {
        public string id { get; set; }
    }
}
