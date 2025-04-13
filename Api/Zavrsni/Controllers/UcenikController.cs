using Microsoft.AspNetCore.Mvc;
using Gis.Api.Repositories;
namespace Gis.Api.Controllers;


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

    [HttpGet("svi")]
    public async Task<IActionResult> GetAllUcenici()
    {
        var ucenici = await _ucenikRepository.GetAllUcenici();

        return Ok(ucenici);
    }
}
