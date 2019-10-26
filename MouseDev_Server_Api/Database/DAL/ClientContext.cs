using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MouseDev_Server_Api.Database.Models;

namespace MouseDev_Server_Api.Database.DAL
{
    public class ClientContext
    {
        private readonly IMongoDatabase _database = null;

        public ClientContext(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DB_Name);
        }

        public IMongoCollection<Client> Clients
        {
            get
            {
                return _database.GetCollection<Client>("Client");
            }
        }
    }
}
