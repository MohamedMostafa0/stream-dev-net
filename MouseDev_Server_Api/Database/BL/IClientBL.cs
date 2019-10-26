using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MouseDev_Server_Api.Database.Models;

namespace MouseDev_Server_Api.Database.BL
{
    public interface IClientBL
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClient(string id);
        Task<IEnumerable<Client>> GetClientAsync(string email, DateTime createdTime);
        Task AddClient(Client item);
        Task<bool> RemoveClient(string id);
        Task<bool> UpdateClientAsync(string id , DateTime lastTimeOpened);
        Task<bool> RemoveAllClients();
    }
}
