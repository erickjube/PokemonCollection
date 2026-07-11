using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonCollection.Application.Interfaces.Repositories;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Infrastructure.Data;
using PokemonCollection.Infrastructure.ExternalApis;
using PokemonCollection.Infrastructure.Repositories;

namespace PokemonCollection.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connection));

        services.AddScoped<IPokeApiClient, PokeApiClient>();
        services.AddHttpClient<IPokeApiClient, PokeApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        });

        services.AddScoped<IPokemonTcgClient, PokemonTcgClient>();
        services.AddHttpClient<IPokemonTcgClient, PokemonTcgClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.pokemontcg.io/v2/");

            client.DefaultRequestHeaders.Add("X-Api-Key", config["PokemonTcg:ApiKey"]);
        });

        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
