using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Gis.Api.Models;
namespace Gis.Api.Data
{

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        
        public IMongoDatabase Database => _database;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDB"));
            _database = client.GetDatabase("Sarajevo");
        }

        public IMongoCollection<Ucenik> GetUceniciCollection()
        {
            return _database.GetCollection<Ucenik>("UceniciPoLokacijama");
        }

        public IMongoCollection<Skola> GetSkoleCollection()
        {
            return _database.GetCollection<Skola>("Skole");
        }
    }

}
