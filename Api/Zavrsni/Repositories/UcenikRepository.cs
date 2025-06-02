using Gis.Api.Data;
using MongoDB.Driver;
using Gis.Api.Models;
using MongoDB.Bson;

namespace Gis.Api.Repositories
{
    public class UcenikRepository
    {
        private readonly MongoDbContext _context;
        private const string SarajevoLocation = "Sarajevo";

        public UcenikRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ucenik>> GetUcenici(int limit)
        {
            var regexFilter = Builders<Ucenik>.Filter.Regex(
                ucenik => ucenik.Teritorija,
                new BsonRegularExpression("Sarajevo|Bosna i Hercegovina", "i")
            );

            return await _context.GetUceniciCollection()
                                 .Find(regexFilter)
                                 .Limit(limit)
                                 .ToListAsync();
        }


        public async Task<List<Ucenik>> GetUceniciByLocation(string location)
        {
            var locationFilter = string.IsNullOrEmpty(location) ? 
                SarajevoLocation : 
                $"{SarajevoLocation}.*{location}";
                
            var filter = Builders<Ucenik>.Filter.Regex(
                ucenik => ucenik.Teritorija,
                new MongoDB.Bson.BsonRegularExpression(locationFilter, "i")
            );

            return await _context.GetUceniciCollection()
                                 .Find(filter)
                                 .ToListAsync();
        }

        public async Task<List<Ucenik>> GetFilteredUcenici(UcenikFilter filter)
        {
            var filterBuilder = Builders<Ucenik>.Filter;
            var filters = new List<FilterDefinition<Ucenik>>();
            
            // Always add the Sarajevo filter
            filters.Add(filterBuilder.Eq(u => u.Teritorija, SarajevoLocation));

            // Apply additional filters only if they are provided
            if (filter.Level.HasValue)
            {
                filters.Add(filterBuilder.Eq(u => u.Level, filter.Level.Value));
            }

            if (!string.IsNullOrEmpty(filter.Starost))
            {
                filters.Add(filterBuilder.Regex(u => u.Starost, 
                    new MongoDB.Bson.BsonRegularExpression(filter.Starost, "i")));
            }

            if (!string.IsNullOrEmpty(filter.Spol))
            {
                filters.Add(filterBuilder.Regex(u => u.Spol, 
                    new MongoDB.Bson.BsonRegularExpression(filter.Spol, "i")));
            }

            if (!string.IsNullOrEmpty(filter.EducationStatus))
            {
                var educationFilters = new List<FilterDefinition<Ucenik>>();
                var regex = new MongoDB.Bson.BsonRegularExpression(filter.EducationStatus, "i");
                
                educationFilters.Add(filterBuilder.Regex(u => u.NeSkolujeSe, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.PredskolskoObrazovanje, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.OsnovnaSkola, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.SrednjaSkola, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.SpecijalizacijaPoslijeSrednje, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.VisaSkola, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramOsnovne, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramSpecijalisticke, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramMagistarske, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramDoktorske, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaI, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaII, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaIntegrisani, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaIII, regex));
                
                filters.Add(filterBuilder.Or(educationFilters));
            }

            // Combine all filters with AND logic
            var combinedFilter = filterBuilder.And(filters);

            // Create sort definition
            SortDefinition<Ucenik> sortDefinition;
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                var property = typeof(Ucenik).GetProperty(filter.SortBy);
                if (property != null)
                {
                    sortDefinition = filter.SortDescending
                        ? Builders<Ucenik>.Sort.Descending(filter.SortBy)
                        : Builders<Ucenik>.Sort.Ascending(filter.SortBy);
                }
                else
                {
                    sortDefinition = filter.SortDescending
                        ? Builders<Ucenik>.Sort.Descending(u => u.Teritorija)
                        : Builders<Ucenik>.Sort.Ascending(u => u.Teritorija);
                }
            }
            else
            {
                sortDefinition = Builders<Ucenik>.Sort.Ascending(u => u.Teritorija);
            }

            var query = _context.GetUceniciCollection().Find(combinedFilter);
            
            if (filter.Skip.HasValue)
            {
                query = query.Skip(filter.Skip.Value);
            }
            
            query = query.Limit(filter.Limit ?? 100).Sort(sortDefinition);
            
            return await query.ToListAsync();
        }

        public async Task<long> GetFilteredCount(UcenikFilter filter)
        {
            var filterBuilder = Builders<Ucenik>.Filter;
            var filters = new List<FilterDefinition<Ucenik>>();
            
            // Always add the Sarajevo filter
            filters.Add(filterBuilder.Eq(u => u.Teritorija, SarajevoLocation));

            // Apply additional filters only if they are provided
            if (filter.Level.HasValue)
            {
                filters.Add(filterBuilder.Eq(u => u.Level, filter.Level.Value));
            }

            if (!string.IsNullOrEmpty(filter.Starost))
            {
                filters.Add(filterBuilder.Regex(u => u.Starost, 
                    new MongoDB.Bson.BsonRegularExpression(filter.Starost, "i")));
            }

            if (!string.IsNullOrEmpty(filter.Spol))
            {
                filters.Add(filterBuilder.Regex(u => u.Spol, 
                    new MongoDB.Bson.BsonRegularExpression(filter.Spol, "i")));
            }

            if (!string.IsNullOrEmpty(filter.EducationStatus))
            {
                var educationFilters = new List<FilterDefinition<Ucenik>>();
                var regex = new MongoDB.Bson.BsonRegularExpression(filter.EducationStatus, "i");
                
                educationFilters.Add(filterBuilder.Regex(u => u.NeSkolujeSe, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.PredskolskoObrazovanje, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.OsnovnaSkola, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.SrednjaSkola, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.SpecijalizacijaPoslijeSrednje, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.VisaSkola, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramOsnovne, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramSpecijalisticke, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramMagistarske, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.StariProgramDoktorske, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaI, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaII, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaIntegrisani, regex));
                educationFilters.Add(filterBuilder.Regex(u => u.ProgramBolonjaIII, regex));
                
                filters.Add(filterBuilder.Or(educationFilters));
            }

            var combinedFilter = filterBuilder.And(filters);

            return await _context.GetUceniciCollection().CountDocumentsAsync(combinedFilter);
        }
    }
}
