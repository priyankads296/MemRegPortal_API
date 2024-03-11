using MemRegPortal.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.DataAccessLayer
{
   
    public class AuthenticationDL : IAuthenticationDL
    {
        private readonly IConfiguration _configuration;        //If we want any string to fetch from appsettings.json we use IConfiguration
        private readonly MongoClient _mongoClient;               //establish connection with database
        private readonly IMongoCollection<UserRequest> _userCollection;   //instance to establish connection with collection <>describes the schema in which dtabase is made
      

        public AuthenticationDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["MemberDatabaseSettings:ConnectionString"]);      //connection with database software established using connection string
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["MemberDatabaseSettings:DatabaseName"]);   //connection with database established
            _userCollection = _MongoDatabase.GetCollection<UserRequest>(_configuration["MemberDatabaseSettings:UserCollectionName"]);
            
        }

        public async Task<UserResponse> Register(UserRequest user)
        {
            UserResponse res = new UserResponse();
            res.IsSuccess = true;
            res.Message = "User Created Successfully";
            try
            {
                UserRequest existingUser = new UserRequest();
                existingUser = await _userCollection.Find(u => u.Username == user.Username).FirstOrDefaultAsync();
                if(existingUser!=null)
                {
                    res.Message = "User already exists!";
                    return res;
                }
                await _userCollection.InsertOneAsync(user);
            }
            catch(Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return res;
           
        }

        public async Task<LoginResponse> Login(LoginRequest user)
        {
            LoginResponse res = new LoginResponse();
            res.IsSuccess = true;
            res.Message = "Login Successful";
            try
            {
                UserRequest existingUser = new UserRequest();
                existingUser = await _userCollection.Find(u => u.Username == user.UserName && u.Password == user.Password).FirstOrDefaultAsync();
                if (existingUser == null)

                {
                    res.Message = "User doesn't exist!";
                    return res;
                }
                var jwtService = new JWTService(_configuration);
                res.JWTToken = jwtService.GenerateToken(
                    existingUser.UserID.ToString(),
                    existingUser.Firstname,
                    existingUser.Lastname,
                    existingUser.Username,
                    existingUser.PhoneNo
                    );
                


            }
            catch(Exception ex)
            {
                res.IsSuccess = false;
                res.Message = "Exception Occurs : " + ex.Message;
            }
            return res;
           
        }

        
    }
}
