using Microsoft.Extensions.DependencyInjection;
using PokemonCollection.Application.Interfaces.Services;
using PokemonCollection.Application.Services;

namespace PokemonCollection.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPokemonImportService, PokemonImportService>();

        return services;
    }
}
