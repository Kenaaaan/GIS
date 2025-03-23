using MongoDB.Driver;
using Gis.Api.Data;
using Gis.Api.Models;

namespace Zavrsni.Services
{
    public class MongoDbService
    {
        private readonly MongoDbContext _context;

        public MongoDbService(MongoDbContext context)
        {
            _context = context;
        }

        public IMongoCollection<Ucenik> GetUceniciCollection()
        {
            return _context.GetUceniciCollection();
        }
    }

}
