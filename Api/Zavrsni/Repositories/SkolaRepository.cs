using Gis.Api.Data;
using Gis.Api.Models;
using MongoDB.Driver;

namespace Gis.Api.Repositories
{
    public class SkolaRepository
    {
        private readonly MongoDbContext _context;

        public SkolaRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skola>> GetSkole(int limit)
        {
            return await _context.GetSkoleCollection()
                                 .Find(_ => true)
                                 .Limit(limit)
                                 .ToListAsync();
        }

    }
}
