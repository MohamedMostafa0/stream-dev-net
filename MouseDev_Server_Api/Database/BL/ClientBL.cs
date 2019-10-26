using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MouseDev_Server_Api.Database.DAL;
using MouseDev_Server_Api.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MouseDev_Server_Api.Database.BL
{
    public class ClientBL : IClientBL
    {
        private readonly ClientContext _context = null;

        public ClientBL(IOptions<DBSettings> settings)
        {
            _context = new ClientContext(settings);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            try
            {
                return await _context.Clients
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Client> GetClient(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Clients
                                .Find(Client =>  Client.Id == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after body text, updated time, and header image size
        //
        public async Task<IEnumerable<Client>> GetClientAsync(string email, DateTime lastTimeopened)
        {
            try
            {
                var query = _context.Clients.Find(Client => Client.Email == email &&
                                       Client.LastTimeOpened >= lastTimeopened);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddClient(Client item)
        {
            try
            {
                await _context.Clients.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveClient(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Clients.DeleteOneAsync(
                        Builders<Client>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateClientAsync(string id, DateTime lastTimeOpened)
        {
            var filter = Builders<Client>.Filter.Eq(s => s.Id, GetInternalId(id));
            var update = Builders<Client>.Update
                            .CurrentDate(s => s.LastTimeOpened);

            try
            {
                UpdateResult actionResult
                    = await _context.Clients.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllClients()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Clients.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
