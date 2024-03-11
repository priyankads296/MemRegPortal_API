using MemRegPortal.DataAccessLayer;
using MemRegPortal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemRegPortal.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MemRegController : ControllerBase
    {

        private readonly IMemRegDL _memRegDL;   //dependency injection
        public MemRegController(IMemRegDL memRegDL)
        {
            _memRegDL = memRegDL;       //whatever methods in interface will come under memRegDL
        }

        [HttpPost]
        public async Task<IActionResult> InsertMember(InsertMemberRequest req)

        {
            InsertMemberResponse res = new InsertMemberResponse();
            try
            {
                res = await _memRegDL.InsertMember(req);
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);


        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {
            GetAllRecordResponse res = new GetAllRecordResponse();              //connection established between controller and data access layer
            try
            {
                res = await _memRegDL.GetAllRecord();
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordById([FromQuery]string id)
        {
            GetRecordByIdResponse res = new GetRecordByIdResponse();
            try
            {
                res = await _memRegDL.GetRecordById(id);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetRecordByName([FromQuery]string name)
        {
            GetRecordByNameResponse res = new GetRecordByNameResponse();
            try
            {
                res = await _memRegDL.GetRecordByName(name);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMemberByName( InsertMemberRequest updatedMember)
        {

             UpdateMemberByNameResponse res = new UpdateMemberByNameResponse();              //connection established between controller and data access layer
                try
                {
                    res = await _memRegDL.UpdateMemberByName(updatedMember);
                }
                catch (Exception ex)
                {
                    res.IsSuccess = false;
                    res.Message = "Exception Occurs : " + ex.Message;
                }
                return Ok(res);
        }
        

       
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(string name, ClaimRequest request)
        {
            ClaimResponse res = new ClaimResponse();
            try
            {
                res = await _memRegDL.SubmitClaim(name, request);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllClaim()
        {
            GetAllClaimResponse res = new GetAllClaimResponse();              //connection established between controller and data access layer
            try
            {
                res = await _memRegDL.GetAllClaim();
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetClaimByName([FromQuery] string memberName)
        {
            GetClaimByNameResponse res = new GetClaimByNameResponse();
            try
            {
                res = await _memRegDL.GetClaimByName(memberName);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetClaimById([FromQuery] string claimId)
        {
            GetClaimByIdResponse res = new GetClaimByIdResponse();
            try
            {
                res = await _memRegDL.GetClaimById(claimId);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRecordById(string id)
        {
            DeleteRecordByIdResponse res = new DeleteRecordByIdResponse();
            try
            {
                res = await _memRegDL.DeleteRecordById(id);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllRecord()
        {
            DeleteAllRecordResponse res = new DeleteAllRecordResponse();
            try
            {
                res = await _memRegDL.DeleteAllRecord();

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteClaimById(string id)
        {
            DeleteClaimByIdResponse res = new DeleteClaimByIdResponse();
            try
            {
                res = await _memRegDL.DeleteClaimById(id);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(res);

        }




    }
}
