using Microsoft.AspNetCore.Mvc;
using PokemonCollection.Application.Interfaces.Services;

namespace PokemonCollection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonImportController : ControllerBase
{
    private readonly IPokemonImportService _importService;

    public PokemonImportController(IPokemonImportService importService)
    {
        _importService = importService;
    }

    [HttpPost]
    public async Task<ActionResult> Import()
    {
        await _importService.ImportAsync();
        return Ok("Pokémons importados com sucesso!");
    }
}
