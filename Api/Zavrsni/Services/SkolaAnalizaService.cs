using Gis.Api.Models;
using Gis.Api.Repositories;

namespace Gis.Api.Services
{
    public class SkolaAnalizaService
    {
        private readonly SkolaRepository _skolaRepository;

        private const double MIN_LAT = 43.8000;
        private const double MAX_LAT = 43.9000;
        private const double MIN_LON = 18.3000;
        private const double MAX_LON = 18.5000;
        
        private readonly Dictionary<string, double> _populationDensityData = new Dictionary<string, double>
        {
            { "Stari Grad", 7500.0 },
            { "Centar", 9000.0 },
            { "Novo Sarajevo", 8000.0 },
            { "Novi Grad", 7000.0 },
            { "Ilidza", 5000.0 },
            { "Vogosca", 3500.0 },
            { "Hadzici", 2000.0 },
            { "Ilijas", 1500.0 }
        };

        private readonly Dictionary<string, (double lat, double lon)> _keyAreas = new Dictionary<string, (double lat, double lon)>
        {
            { "Stari Grad", (43.8607, 18.4326) },
            { "Centar", (43.8563, 18.4131) },
            { "Novo Sarajevo", (43.8500, 18.3900) },
            { "Novi Grad", (43.8400, 18.3600) },
            { "Ilidza", (43.8300, 18.3100) },
            { "Vogosca", (43.9000, 18.3400) },
            { "Hadzici", (43.8200, 18.2000) },
            { "Ilijas", (43.9600, 18.2700) }
        };

        public SkolaAnalizaService(SkolaRepository skolaRepository)
        {
            _skolaRepository = skolaRepository;
        }

        public async Task<SkolaAnalysisResult> AnalyzeSchoolLocations(SkolaAnalysisRequest request)
        {
            var existingSchools = await _skolaRepository.GetSkole(); 
            
            var existingSchoolCoordinates = existingSchools
                .Select(s => (lat: s.Geometry.Coordinates[1], lon: s.Geometry.Coordinates[0]))
                .ToList();
            
            var gridResolution = 0.005; 
            var candidateLocations = new List<(double lat, double lon, double score)>();
            
            for (double lat = MIN_LAT; lat <= MAX_LAT; lat += gridResolution)
            {
                for (double lon = MIN_LON; lon <= MAX_LON; lon += gridResolution)
                {
                    var score = CalculateLocationScore(lat, lon, existingSchoolCoordinates, request);
                    
                    if (score > 0)
                    {
                        candidateLocations.Add((lat, lon, score));
                    }
                }
            }
            
            var topLocations = candidateLocations
                .OrderByDescending(l => l.score)
                .Take(request.MaxLocations)
                .ToList();

            var result = new SkolaAnalysisResult
            {
                RecommendedLocations = topLocations.Select((loc, index) => 
                    new SkolaAnaliza
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = $"Recommended School Location #{index + 1}",
                        Latitude = loc.lat,
                        Longitude = loc.lon,
                        Score = loc.score,
                        Reason = GenerateReasonText(loc.lat, loc.lon, existingSchoolCoordinates, request),
                        AdditionalData = new Dictionary<string, object>
                        {
                            { "NearestNeighborDistance", GetNearestNeighborDistance(loc.lat, loc.lon, existingSchoolCoordinates) },
                            { "NearestNeighborhood", GetNearestNeighborhood(loc.lat, loc.lon) }
                        }
                    }).ToList(),
                AnalysisMetadata = new Dictionary<string, object>
                {
                    { "TotalExistingSchools", existingSchools.Count },
                    { "AnalysisDate", DateTime.UtcNow },
                    { "MinimumDistanceThreshold", request.MinimumDistanceThreshold },
                    { "ConsideredPopulationDensity", request.ConsiderPopulationDensity }
                }
            };
            
            return result;
        }

        private double CalculateLocationScore(double lat, double lon, List<(double lat, double lon)> existingSchools, SkolaAnalysisRequest request)
        {
            double nearestSchoolDistance = GetNearestNeighborDistance(lat, lon, existingSchools);
            if (nearestSchoolDistance < request.MinimumDistanceThreshold)
            {
                return 0; 
            }

            double score = 50.0;

            if (request.ConsiderExistingSchools)
            {
                score += Math.Min(nearestSchoolDistance * 20, 30); 
            }

            if (request.ConsiderPopulationDensity)
            {
                string nearestNeighborhood = GetNearestNeighborhood(lat, lon);
                if (_populationDensityData.TryGetValue(nearestNeighborhood, out double density))
                {
                    double maxDensity = _populationDensityData.Values.Max();
                    double densityScore = (density / maxDensity) * 20;
                    score += densityScore;
                }
            }

            return score;
        }

        private double GetNearestNeighborDistance(double lat, double lon, List<(double lat, double lon)> points)
        {
            if (!points.Any())
                return double.MaxValue;

            return points.Min(p => CalculateDistance(lat, lon, p.lat, p.lon));
        }

        private string GetNearestNeighborhood(double lat, double lon)
        {
            return _keyAreas
                .OrderBy(area => CalculateDistance(lat, lon, area.Value.lat, area.Value.lon))
                .First().Key;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; 
            
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);
            
            double a = 
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) * 
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            
            return distance;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private string GenerateReasonText(double lat, double lon, List<(double lat, double lon)> existingSchools, SkolaAnalysisRequest request)
        {
            double nearestDistance = GetNearestNeighborDistance(lat, lon, existingSchools);
            string neighborhood = GetNearestNeighborhood(lat, lon);
            
            string reason = $"This location in {neighborhood} is {nearestDistance:F2} km from the nearest existing school. ";
            
            if (request.ConsiderPopulationDensity && _populationDensityData.TryGetValue(neighborhood, out double density))
            {
                reason += $"The area has a population density of approximately {density:F0} people per km². ";
            }
            
            return reason + "Based on the spatial analysis, this location would provide good coverage for the surrounding area.";
        }
    }
}