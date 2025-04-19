using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gis.Api.Models
{
    public class SkolaAnaliza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public double Score { get; set; }
        
        public string Reason { get; set; }
        
        public Dictionary<string, object> AdditionalData { get; set; }
    }

    public class SkolaAnalysisRequest
    {
        public int MaxLocations { get; set; } = 5;
        
        public double MinimumDistanceThreshold { get; set; } = 1.0; // in km
        
        public bool ConsiderPopulationDensity { get; set; } = true;
        
        public bool ConsiderExistingSchools { get; set; } = true;
    }

    public class SkolaAnalysisResult
    {
        public List<SkolaAnaliza> RecommendedLocations { get; set; }
        
        public Dictionary<string, object> AnalysisMetadata { get; set; }
    }
}