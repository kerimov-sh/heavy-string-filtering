using HeavyStringFiltering.Business.DataTransferObjects;
using HeavyStringFiltering.DataAccess;

namespace HeavyStringFiltering.Business.Services.Implementations;

public class FilteringWorkerService : IFilteringWorkerService
{
    private readonly IFilterService _filterService;
    private readonly IFilteredTextStorage _filteredTextStorage;

    public FilteringWorkerService(
        IFilterService filterService,
        IFilteredTextStorage filteredTextStorage)
    {
        _filterService = filterService;
        _filteredTextStorage = filteredTextStorage;
    }

    public Task ProcessAsync(UploadedRawStringDto uploadedRawString)
    {
        var filtered = _filterService.Filter(uploadedRawString.RawString);
        _filteredTextStorage.Save(uploadedRawString.UploadId, filtered);

        return Task.CompletedTask;
    }
}