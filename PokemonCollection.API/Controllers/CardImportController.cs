using Microsoft.AspNetCore.Mvc;
using PokemonCollection.Application.Interfaces.Services;

namespace PokemonCollection.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardImportController : ControllerBase
{
    private readonly ICardImportService _importService;

    public CardImportController(ICardImportService importService)
    {
        _importService = importService;
    }

    [HttpPost]
    public async Task<ActionResult> Import()
    {
        await _importService.ImportAsync();
        return Ok("Cartas importadas com sucesso!");
    }
}
