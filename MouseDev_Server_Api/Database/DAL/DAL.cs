using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MouseDev_Server_Api.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MouseDev_Server_Api.Database.DAL
{
    public static class DAL<T> where T : BaseModel
    {
        private static IMongoDatabase _database = null;
        public static void Init(IOptions<DBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DB_Name);
        }

        private static IMongoCollection<T> GetCollection(string name)
        {
            return _database.GetCollection<T>(name);
        }
        private static ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public static async Task<IEnumerable<T>> GetAll(string name)
        {
            try
            {
                return await GetCollection(name)
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public static async Task<T> GetById(string name , string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await GetCollection(name)
                                .Find(obj => obj.Id == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
