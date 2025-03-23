using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Gis.Api.Models;
namespace Gis.Api.Data
{

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDB"));
            _database = client.GetDatabase("Sarajevo");
        }

        public IMongoCollection<Ucenik> GetUceniciCollection()
        {
            return _database.GetCollection<Ucenik>("UceniciPoLokacijama");
        }
    }

}
