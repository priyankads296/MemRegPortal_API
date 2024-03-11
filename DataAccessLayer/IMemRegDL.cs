using MemRegPortal.Controllers;
using MemRegPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.DataAccessLayer
{
    public interface IMemRegDL
    {
        public Task<InsertMemberResponse> InsertMember(InsertMemberRequest request);
        public Task<GetAllRecordResponse> GetAllRecord();
        public Task<GetRecordByIdResponse> GetRecordById(string id);
        public Task<GetRecordByNameResponse> GetRecordByName(string name);
        public Task<UpdateMemberByNameResponse> UpdateMemberByName( InsertMemberRequest updatedMember);
        public Task<ClaimResponse> SubmitClaim(string name,ClaimRequest request);
        public Task<GetAllClaimResponse> GetAllClaim();
        public Task<GetClaimByNameResponse> GetClaimByName(string memberName);
        public Task<GetClaimByIdResponse> GetClaimById(string claimId);
        public Task<DeleteRecordByIdResponse> DeleteRecordById(string id);
        public Task<DeleteAllRecordResponse> DeleteAllRecord();
        public Task<DeleteClaimByIdResponse> DeleteClaimById(string id);
    }
}
