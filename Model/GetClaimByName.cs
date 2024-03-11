using MemRegPortal.Model;
using System.Collections.Generic;

namespace MemRegPortal.DataAccessLayer
{
    public class GetClaimByNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ClaimRequest> data { get; set; }
    }
    public class GetClaimByNameRequest
    {
        public string memberName { get; set; }
    }
}