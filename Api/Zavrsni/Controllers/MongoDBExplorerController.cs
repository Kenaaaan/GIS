using Gis.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System.Text.Json;

namespace Gis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoDBExplorerController : ControllerBase
    {
        private readonly MongoDBExplorerService _explorerService;

        public MongoDBExplorerController(MongoDBExplorerService explorerService)
        {
            _explorerService = explorerService;
        }

        [HttpGet("collections/{collectionName}/stats")]
        public async Task<IActionResult> GetCollectionStats(string collectionName)
        {
            var stats = await _explorerService.GetCollectionStats(collectionName);
            return Ok(FormatBsonDocument(stats));
        }

        [HttpPost("query/{collectionName}")]
        public async Task<IActionResult> ExecuteQuery(string collectionName, [FromBody] JsonElement queryJson)
        {
            var result = await _explorerService.ExecuteQuery(collectionName, queryJson.GetRawText());
            return Ok(FormatBsonDocument(result));
        }
        
        private object FormatBsonDocument(BsonDocument doc)
        {
            return BsonTypeMapper.MapToDotNetValue(doc);
        }
        
    }
}