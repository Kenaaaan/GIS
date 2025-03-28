using Gis.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkolaController : ControllerBase
    {
        public readonly SkolaRepository _skolaRepository;

        public SkolaController(SkolaRepository skolaRepository)
        {
            _skolaRepository = skolaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSkole(int limit = 10)
        {
            var skole = await _skolaRepository.GetSkole(limit);
            return Ok(skole);
        }
    }
}