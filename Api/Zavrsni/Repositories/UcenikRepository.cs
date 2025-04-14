using Gis.Api.Data;
using MongoDB.Driver;
using Gis.Api.Models;

namespace Gis.Api.Repositories
{
    public class UcenikRepository
    {
        private readonly MongoDbContext _context;

        public UcenikRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ucenik>> GetUcenici(int limit)
        {
            return await _context.GetUceniciCollection()
                                 .Find(_ => true)  
                                 .Limit(limit)
                                 .ToListAsync();
        }

        public async Task<List<Ucenik>> GetUceniciByLocation(string location)
        {
            var filter = Builders<Ucenik>.Filter.Regex(
                ucenik => ucenik.Teritorija,
                new MongoDB.Bson.BsonRegularExpression(location, "i")
            );

            return await _context.GetUceniciCollection()
                                 .Find(filter)
                                 .ToListAsync();
        }

    }
}
