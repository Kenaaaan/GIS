using Gis.Api.Data;
using Gis.Api.Models;
using MongoDB.Driver;

namespace Gis.Api.Repositories
{
    public class SkolaRepository
    {
        private readonly MongoDbContext _context;
        private const string SarajevoLocation = "Sarajevo";

        public SkolaRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Skola>> GetSkole()
        {
            return await _context.GetSkoleCollection().Find(FilterDefinition<Skola>.Empty).ToListAsync();
        }
    }
}
