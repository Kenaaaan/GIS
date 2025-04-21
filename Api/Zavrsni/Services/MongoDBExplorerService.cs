using MongoDB.Bson;
using MongoDB.Driver;
using Gis.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gis.Api.Services
{
    public class MongoDBExplorerService
    {
        private readonly MongoDbContext _context;

        public MongoDBExplorerService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetCollectionNames()
        {
            var collectionNames = await _context.Database.ListCollectionNames().ToListAsync();
            return collectionNames;
        }

        public async Task<List<BsonDocument>> GetCollectionData(string collectionName, int limit = 100)
        {
            var collection = _context.Database.GetCollection<BsonDocument>(collectionName);
            return await collection.Find(new BsonDocument())
                                 .Limit(limit)
                                 .ToListAsync();
        }

        public async Task<BsonDocument> GetCollectionStats(string collectionName)
        {
            var command = new BsonDocument { { "collStats", collectionName } };
            return await _context.Database.RunCommandAsync<BsonDocument>(command);
        }

        public async Task<BsonDocument> GetDatabaseStats()
        {
            var command = new BsonDocument { { "dbStats", 1 } };
            return await _context.Database.RunCommandAsync<BsonDocument>(command);
        }
        
        public async Task<BsonDocument> ExecuteQuery(string collectionName, string filterJson)
        {
            try
            {
                var collection = _context.Database.GetCollection<BsonDocument>(collectionName);
                var filter = BsonDocument.Parse(filterJson);
                var result = await collection.Find(filter).ToListAsync();
                
                return new BsonDocument 
                {
                    { "success", true },
                    { "result", new BsonArray(result) },
                    { "count", result.Count }
                };
            }
            catch (Exception ex)
            {
                return new BsonDocument
                {
                    { "success", false },
                    { "error", ex.Message }
                };
            }
        }
    }
}