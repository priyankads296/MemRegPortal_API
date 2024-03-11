using MemRegPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemRegPortal.DataAccessLayer
{
    public interface IAuthenticationDL
    {
        public Task<UserResponse> Register(UserRequest user);
        public Task<LoginResponse> Login(LoginRequest user);
    }
}
