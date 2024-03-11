using MemRegPortal.Model;

namespace MemRegPortal.DataAccessLayer
{
    public class GetClaimByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ClaimRequest data { get; set; }
    }
    public class GetClaimByIdRequest
    {
        public string id { get; set; }
    }
}