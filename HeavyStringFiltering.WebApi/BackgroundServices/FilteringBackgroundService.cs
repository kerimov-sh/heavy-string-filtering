using HeavyStringFiltering.Business.DataTransferObjects;
using HeavyStringFiltering.Business.Services;
using HeavyStringFiltering.DataAccess;

namespace HeavyStringFiltering.WebApi.BackgroundServices;

public class FilteringBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FilteringBackgroundService> _logger;

    public FilteringBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<FilteringBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var filteringWorkerService = scope.ServiceProvider
                .GetRequiredService<IFilteringWorkerService>();
            var uploadedRawStringQueue = scope.ServiceProvider
                .GetRequiredService<IQueue<UploadedRawStringDto>>();

            if (uploadedRawStringQueue.TryDequeue(out var item) &&
                item is not null)
            {
                try
                {
                    await filteringWorkerService.ProcessAsync(item);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Filtering failed. uploadId: {item.UploadId}");
                }
            }
            else
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}