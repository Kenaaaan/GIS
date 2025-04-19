using Gis.Api.Models;
using Gis.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkolaAnalizaController : ControllerBase
    {
        private readonly SkolaAnalizaService _skolaAnalizaService;

        public SkolaAnalizaController(SkolaAnalizaService skolaAnalizaService)
        {
            _skolaAnalizaService = skolaAnalizaService;
        }

        /// <summary>
        /// Analyze where new schools should be built in Sarajevo based on existing locations
        /// </summary>
        /// <param name="request">Analysis request parameters</param>
        /// <returns>Recommended locations for new schools</returns>
        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeSchoolLocations([FromBody] SkolaAnalysisRequest request)
        {
            if (request == null)
            {
                request = new SkolaAnalysisRequest(); // Use default parameters
            }

            var result = await _skolaAnalizaService.AnalyzeSchoolLocations(request);
            return Ok(result);
        }

        /// <summary>
        /// Get recommended school locations with default parameters
        /// </summary>
        /// <param name="maxLocations">Maximum number of locations to return (default: 5)</param>
        /// <returns>Recommended locations for new schools</returns>
        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations(int maxLocations = 5)
        {
            var request = new SkolaAnalysisRequest
            {
                MaxLocations = maxLocations,
                ConsiderPopulationDensity = true,
                ConsiderExistingSchools = true,
                MinimumDistanceThreshold = 1.0
            };

            var result = await _skolaAnalizaService.AnalyzeSchoolLocations(request);
            return Ok(result);
        }
    }
}