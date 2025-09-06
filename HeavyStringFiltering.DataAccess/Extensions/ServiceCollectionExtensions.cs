using HeavyStringFiltering.DataAccess.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace HeavyStringFiltering.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDataAccessDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IFilteredTextStorage, InMemoryFilteredTextStorage>();
        services.AddSingleton(typeof(IQueue<>), typeof(InMemoryQueue<>));
    }
}