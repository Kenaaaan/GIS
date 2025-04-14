using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gis.Api.Models
{
    public class Skola
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } = "Feature";

        [BsonElement("properties")]
        public SkolaProperties Properties { get; set; }

        [BsonElement("geometry")]
        public Geometry Geometry { get; set; }

        [BsonElement("id")]
        public string id {get; set;} 
    }

    public class SkolaProperties
    {
        [BsonElement("@id")]
        public string FeatureId { get; set; }

        [BsonElement("amenity")]
        public string Amenity { get; set; }

        [BsonElement("barrier")]
        public string? barrier { get; set; }

        [BsonElement("name:hr")]
        public string? nameHr { get; set; }

        [BsonElement("name:sr")]
        public string? nameSr { get; set; }

        [BsonElement("addr:city")]
        public string? addr { get; set; }

        [BsonElement("addr:housenumber")]
        public string? addrHouseNumber { get; set; }

        [BsonElement("addr:street")]
        public string? addrStreet { get; set; }

        [BsonElement("contact:email")]
        public string? contactEmail { get; set; }

        [BsonElement("email")]
        public string? email { get; set; }

        [BsonElement("denomination")]
        public string? denomination { get; set; }

        [BsonElement("operator")]
        public string? operatorField { get; set; }

        [BsonElement("opening_hours")]
        public string? openingHours { get; set; }

        [BsonElement("contact:phone")]
        public string? Contactphone { get; set; }

        [BsonElement("contact:website")]
        public string? website { get; set; }

        [BsonElement("building")]
        public string? building { get; set; }

        [BsonElement("source")]
        public string? source { get; set; }

        [BsonElement("website")]
        public string? websiteSchool { get; set; }

        [BsonElement("fence_type")]
        public string? fenceType { get; set; }

        [BsonElement("addr:postcode")]
        public string? postcode { get; set; }

        [BsonElement("old_name")]
        public string? oldName { get; set; }

        [BsonElement("wheelchair")]
        public string? wheelchair { get; set; }
        
        [BsonElement("start_date")]
        public string? startDate { get; set; }
        
        [BsonElement("name:fr")]
        public string? nameFr { get; set; }
        
        [BsonElement("school")]
        public string? school { get; set; }

        [BsonElement("operator:type")]
        public string? operatorType { get; set; }

        [BsonElement("survey:date")]
        public string? surveyDate { get; set; }

        [BsonElement("building:levels")]
        public string? buildingLevels { get; set; }

        [BsonElement("wikidata")]
        public string? wikiData { get; set; }

        [BsonElement("isced:level")]
        public string? iscedLevel { get; set; }

        [BsonElement("grades")]
        public string? grades { get; set; }

        [BsonElement("religion")]
        public string? religion { get; set; }

        [BsonElement("phone")]
        public string? phone { get; set; }

        [BsonElement("wikimedia_commons")]
        public string? wikimediaCommons { get; set; }

        [BsonElement("addr:country")]
        public string? addrCountry { get; set; }

        [BsonElement("contact:fax")]
        public string? contactFax { get; set; }

        [BsonElement("wikipedia")]
        public string? wikipedia { get; set; }

        [BsonElement("layer")]
        public string? layer { get; set; }

        [BsonElement("check_date")]
        public string? checkDate { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("@geometry")]
        public string GeometryType { get; set; }
    }

    public class Geometry
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("coordinates")]
        public double[] Coordinates { get; set; }
    }
}
