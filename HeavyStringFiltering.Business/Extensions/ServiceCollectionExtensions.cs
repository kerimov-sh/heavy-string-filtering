using HeavyStringFiltering.Business.Filtering;
using HeavyStringFiltering.Business.Filtering.Implementations;
using HeavyStringFiltering.Business.Options;
using HeavyStringFiltering.Business.Services;
using HeavyStringFiltering.Business.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HeavyStringFiltering.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBusinessDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<FilteringOptions>(configuration.GetSection(nameof(FilteringOptions)));
        services.AddSingleton<IChunkAssemblerService, ChunkAssemblerService>();
        services.AddScoped<ISimilarityCalculatorFactory, DefaultSimilarityCalculatorFactory>();
        services.AddScoped<IStringChunkService, StringChunkService>();
        services.AddScoped<IFilterService, FilterService>();
        services.AddScoped<IFilteringWorkerService, FilteringWorkerService>();
    }
}