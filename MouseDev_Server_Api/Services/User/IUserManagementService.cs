using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MouseDev_Server_Api.Services.User
{
    public interface IUserManagementService
    {
        bool IsValidUser(string username, string password);
    }
}
