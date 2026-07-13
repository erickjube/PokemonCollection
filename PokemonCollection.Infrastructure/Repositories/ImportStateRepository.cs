using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Domain.Entities;
using PokemonCollection.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace PokemonCollection.Infrastructure.Repositories;

public class ImportStateRepository : IImportStateRepository
{
    private readonly AppDbContext _context;

    public ImportStateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ImportState?> GetAsync()
    {
        return await _context.ImportStates.FirstOrDefaultAsync();
    }

    public async Task AddAsync(ImportState state)
    {
        await _context.ImportStates.AddAsync(state);
    }
}
