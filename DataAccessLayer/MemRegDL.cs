using MemRegPortal.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemRegPortal.DataAccessLayer
{
    public class MemRegDL : IMemRegDL
    {

        private readonly IConfiguration _configuration;        //If we want any string to fetch from appsettings.json we use IConfiguration
        private readonly MongoClient _mongoClient;               //establish connection with database
        private readonly IMongoCollection<InsertMemberRequest> _memberCollection;   //instance to establish connection with collection <>describes the schema in which dtabase is made
        private readonly IMongoCollection<ClaimRequest> _claimCollection;

        public MemRegDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["MemberDatabaseSettings:ConnectionString"]);      //connection with database software established using connection string
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["MemberDatabaseSettings:DatabaseName"]);   //connection with database established
            _memberCollection = _MongoDatabase.GetCollection<InsertMemberRequest>(_configuration["MemberDatabaseSettings:MemberCollectionName"]);
            _claimCollection = _MongoDatabase.GetCollection<ClaimRequest>(_configuration["MemberDatabaseSettings:ClaimCollectionName"]);
        }

        public async Task<InsertMemberResponse> InsertMember(InsertMemberRequest request)
        {
            InsertMemberResponse res = new InsertMemberResponse();
            res.IsSuccess = true;
            res.Message = "";
            try
            {

                request.CreatedDate = DateTime.Now.ToString();
                request.UpdatedDate = string.Empty;
                // request.Status = "Submitted";
                request.Id = GenerateId();
                res.Message = "200 OK: Data Successfully Inserted.\n Member "+request.Id+" generated";
                await _memberCollection.InsertOneAsync(request);

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }

        public async Task<GetAllRecordResponse> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<InsertMemberRequest>();
                response.data = await _memberCollection.Find(x => true).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs." + ex.Message;
            }
            return response;
        }
        public async Task<GetRecordByIdResponse> GetRecordById(string id)
        {
            GetRecordByIdResponse res = new GetRecordByIdResponse();
            res.IsSuccess = true;
            res.Message = "Data Fetch Successfully By Id";
            try
            {
                res.data = await _memberCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (res.data == null)
                {
                    res.Message = "Invalid Id!\n Please Enter Valid Id. ";
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }
        public async Task<GetRecordByNameResponse> GetRecordByName(string name)
        {
            GetRecordByNameResponse res = new GetRecordByNameResponse();
            res.IsSuccess = true;
            res.Message = "Data Fetch Successfully By Name";
            try
            {
                res.data = await _memberCollection.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();
                
                if (res.data==null)
                {
                    res.Message = "Invalid Name!\n Please Enter Valid Name. ";
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }
        
        public async Task<UpdateMemberByNameResponse> UpdateMemberByName(InsertMemberRequest updatedMember)
        {
            UpdateMemberByNameResponse res = new UpdateMemberByNameResponse();
            res.IsSuccess = true;
            res.Message = "Update Record Successfully By Name.";
            try
            {
                GetRecordByNameResponse res1 = await GetRecordByName(updatedMember.Name);
                if(res1.data==null)
                {
                    res.Message = "Input Id Not Found ";
                    return res;
                }
                updatedMember.CreatedDate = res1.data.CreatedDate;
                updatedMember.UpdatedDate = DateTime.Now.ToString();

                var result = await _memberCollection.ReplaceOneAsync(x => x.Name == updatedMember.Name, updatedMember);
                if (!result.IsAcknowledged)
                {
                    res.Message = "Input Id Not Found / Updation Not Occurs";
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
           
        }
       
  
        public async Task<ClaimResponse> SubmitClaim(string name,ClaimRequest request)
        {
            ClaimResponse res = new ClaimResponse();
            res.IsSuccess = true;
            GetRecordByNameResponse res2 = new GetRecordByNameResponse();
            
            var member = res2.data;
            
            try
            {
                //member=await _memberCollection.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();
                member = await _memberCollection.Find(x => x.Name.Equals(name)).FirstOrDefaultAsync();
                request.Id = GenerateRandomClaimId();
                
                if(member!=null)
                {
                    request.DOB = member.DOB;
                }
               
                res.Message = "200 OK: Claim Successfully Generated for " + name + " with Claim Id as " + request.Id;
                await _claimCollection.InsertOneAsync(request);
                

                    
                

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;

        }
        public async Task<GetAllClaimResponse> GetAllClaim()
        {
            GetAllClaimResponse response = new GetAllClaimResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<ClaimRequest>();
                response.data = await _claimCollection.Find(x => true).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs." + ex.Message;
            }
            return response;
        }
        public async Task<GetClaimByNameResponse> GetClaimByName(string memberName)
        {
            GetClaimByNameResponse res = new GetClaimByNameResponse();
            res.IsSuccess = true;
            res.Message = "Data Fetch Successfully By Name";
            try
            {
                res.data = await _claimCollection.Find(x => x.MemberName.Equals(memberName, StringComparison.OrdinalIgnoreCase)).ToListAsync();

                if (res.data == null)
                {
                    res.Message = "Invalid Name!\n Please Enter Valid Name. ";
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }
        public async Task<GetClaimByIdResponse> GetClaimById(string claimId)
        {
            GetClaimByIdResponse res = new GetClaimByIdResponse();
            res.IsSuccess = true;
            res.Message = "Data Fetch Successfully By Id";
            try
            {
                res.data = await _claimCollection.Find(x => x.Id == claimId).FirstOrDefaultAsync();
                if (res.data == null)
                {
                    res.Message = "Invalid Id!\n Please Enter Valid Id. ";
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }

        public async Task<DeleteRecordByIdResponse> DeleteRecordById(string id)
        {
            DeleteRecordByIdResponse res = new DeleteRecordByIdResponse();
            res.IsSuccess = true;
            res.Message = "200 OK: Record Successfully Deleted. ";
            GetRecordByIdResponse res2 = new GetRecordByIdResponse();
            try
            {
                res2.data = await _memberCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (res2.data == null)
                {
                    res.Message = "Member not found! Please enter valid Id. ";
                    return res;
                }

                var Result=await _memberCollection.DeleteOneAsync(x=>x.Id==id);
                
               
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }
        public async Task<DeleteAllRecordResponse> DeleteAllRecord()
        {
            DeleteAllRecordResponse res = new DeleteAllRecordResponse();
            res.IsSuccess = true;
            res.Message = "200 OK: All Records Successfully Deleted. ";
            
            try
            {
             
                var Result = await _memberCollection.DeleteManyAsync(x => true);
                if(Result.DeletedCount==0)
                {
                    res.Message = "No Records found!";
                    return res;
                }


            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }
        public async Task<DeleteClaimByIdResponse> DeleteClaimById(string id)
        {
            DeleteClaimByIdResponse res = new DeleteClaimByIdResponse();
            res.IsSuccess = true;
            res.Message = "200 OK: Record Successfully Deleted. ";
            GetClaimByIdResponse res2 = new GetClaimByIdResponse();
            try
            {
                res2.data = await _claimCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (res2.data == null)
                {
                    res.Message = "Member not found! Please enter valid Id. ";
                    return res;
                }

                var Result = await _claimCollection.DeleteOneAsync(x => x.Id == id);
                

            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;

            }
            return res;
        }

        private string GenerateId()
        {
            var random = new Random();
            int randomId = random.Next(100, 1000);

            string formattedId = randomId.ToString("D3");
            return "R-" + formattedId;
        }

        public string GenerateRandomClaimId()
        {
            var random = new Random();
            StringBuilder randomIdBuilder = new StringBuilder(10);
            for(int i=0;i<10;i++)
            {
                int randomDigit = random.Next(10);
                randomIdBuilder.Append(randomDigit);
            }
            
            return randomIdBuilder.ToString();
        }

        public string GenerateDependentId()
        {
            var random = new Random();
            int randomId = random.Next(100, 1000);

            string formattedId = randomId.ToString("D3");
            return "D-" + formattedId;
        }

   
    }
}
