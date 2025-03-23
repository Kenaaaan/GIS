namespace Gis.Api.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System.Globalization;

    public class Ucenik
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Level { get; set; }

        public string Teritorija { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Starost { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Spol { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Ukupno { get; set; }

        [BsonElement("Ne školuje se ")]
        [BsonRepresentation(BsonType.String)]
        public string NeSkolujeSe { get; set; }

        [BsonElement("Predškolsko obrazovanje  ")]
        [BsonRepresentation(BsonType.String)]
        public string PredskolskoObrazovanje { get; set; }

        [BsonElement("Osnovna škola  ")]
        [BsonRepresentation(BsonType.String)]
        public string OsnovnaSkola { get; set; }

        [BsonElement("Srednja škola  ")]
        [BsonRepresentation(BsonType.String)]
        public string SrednjaSkola { get; set; }

        [BsonElement("Specijalizacija poslije srednje škole  ")]
        [BsonRepresentation(BsonType.String)]
        public string SpecijalizacijaPoslijeSrednje { get; set; }

        [BsonElement("Viša škola ")]
        [BsonRepresentation(BsonType.String)]
        public string VisaSkola { get; set; }

        [BsonElement("Stari program - Osnovne akademske studije")]
        [BsonRepresentation(BsonType.String)]
        public string StariProgramOsnovne { get; set; }

        [BsonElement("Stari program - Specijalističke studije")]
        [BsonRepresentation(BsonType.String)]
        public string StariProgramSpecijalisticke { get; set; }

        [BsonElement("Stari program - Magistarske studije")]
        [BsonRepresentation(BsonType.String)]
        public string StariProgramMagistarske { get; set; }

        [BsonElement("Stari program - Doktorske studije")]
        [BsonRepresentation(BsonType.String)]
        public string StariProgramDoktorske { get; set; }

        [BsonElement("Program po Bolonji - Studij I ciklusa")]
        [BsonRepresentation(BsonType.String)]
        public string ProgramBolonjaI { get; set; }

        [BsonElement("Program po Bolonji - Studij II ciklusa")]
        [BsonRepresentation(BsonType.String)]
        public string ProgramBolonjaII { get; set; }

        [BsonElement("Program po Bolonji - Studij integrisanog I i II ciklusa")]
        [BsonRepresentation(BsonType.String)]
        public string ProgramBolonjaIntegrisani { get; set; }

        [BsonElement("Program po Bolonji - Studij III ciklusa")]
        [BsonRepresentation(BsonType.String)]
        public string ProgramBolonjaIII { get; set; }
    }



}
