using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonCollection.Infrastructure.Data;
using PokemonCollection.Infrastructure.ExternalApis;

namespace PokemonCollection.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connection));

        services.AddHttpClient<PokeApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        });

        return services;
    }
}
