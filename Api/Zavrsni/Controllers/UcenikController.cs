using Microsoft.AspNetCore.Mvc;
using Gis.Api.Repositories;
using Gis.Api.Models;
using System.Threading.Tasks;

namespace Gis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UcenikController : ControllerBase
    {
        private readonly UcenikRepository _ucenikRepository;

        public UcenikController(UcenikRepository ucenikRepository)
        {
            _ucenikRepository = ucenikRepository;
        }

        [HttpGet("limit")]
        public async Task<IActionResult> GetUcenici(int limit = 10)
        {
            var ucenici = await _ucenikRepository.GetUcenici(limit);
          
            return Ok(ucenici);
        }

        [HttpGet("lokacija")]
        public async Task<IActionResult> GetUceniciPoLokacijama(string lokacija)
        {
            var ucenici = await _ucenikRepository.GetUceniciByLocation(lokacija);

            return Ok(ucenici);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredUcenici(
            [FromQuery] int? level = null,
            [FromQuery] string teritorija = null,
            [FromQuery] string starost = null,
            [FromQuery] string spol = null,
            [FromQuery] string educationStatus = null,
            [FromQuery] int? skip = null,
            [FromQuery] int? limit = 100,
            [FromQuery] string sortBy = "Teritorija",
            [FromQuery] bool sortDescending = false)
        {
            var filter = new UcenikFilter
            {
                Level = level,
                Teritorija = teritorija,
                Starost = starost,
                Spol = spol,
                EducationStatus = educationStatus,
                Skip = skip,
                Limit = limit,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var ucenici = await _ucenikRepository.GetFilteredUcenici(filter);
            var totalCount = await _ucenikRepository.GetFilteredCount(filter);

            return Ok(new 
            {
                TotalCount = totalCount,
                Items = ucenici
            });
        }
        
        [HttpPost("filter")]
        public async Task<IActionResult> FilterUcenici([FromBody] UcenikFilter filter)
        {
            if (filter == null)
            {
                filter = new UcenikFilter();
            }

            var ucenici = await _ucenikRepository.GetFilteredUcenici(filter);
            var totalCount = await _ucenikRepository.GetFilteredCount(filter);

            return Ok(new 
            {
                TotalCount = totalCount,
                Items = ucenici
            });
        }
        
        [HttpGet("metadata")]
        public IActionResult GetMetadata()
        {
            // Return available filter options
            return Ok(new
            {
                SortableFields = new[] 
                {
                    "Teritorija", "Level", "Starost", "Spol", "Ukupno", "NeSkolujeSe", 
                    "PredskolskoObrazovanje", "OsnovnaSkola", "SrednjaSkola", 
                    "SpecijalizacijaPoslijeSrednje", "VisaSkola", "StariProgramOsnovne", 
                    "StariProgramSpecijalisticke", "StariProgramMagistarske", "StariProgramDoktorske",
                    "ProgramBolonjaI", "ProgramBolonjaII", "ProgramBolonjaIntegrisani", "ProgramBolonjaIII"
                },
                FilterableFields = new[] 
                {
                    "Level", "Teritorija", "Starost", "Spol", "EducationStatus"
                }
            });
        }
    }
}
