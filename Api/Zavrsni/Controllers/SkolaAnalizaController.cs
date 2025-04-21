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

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeSchoolLocations([FromBody] SkolaAnalysisRequest request)
        {
            if (request == null)
            {
                request = new SkolaAnalysisRequest(); 
            }

            var result = await _skolaAnalizaService.AnalyzeSchoolLocations(request);
            return Ok(result);
        }

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