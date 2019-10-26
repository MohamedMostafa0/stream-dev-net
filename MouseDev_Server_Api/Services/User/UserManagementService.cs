using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MouseDev_Server_Api.Services.User
{
    public class UserManagementService : IUserManagementService
    {
        // mohamed we have to handle the request to login with mongodb and user table
        public bool IsValidUser(string username, string password)
        {
            return true;
        }
    }
}
