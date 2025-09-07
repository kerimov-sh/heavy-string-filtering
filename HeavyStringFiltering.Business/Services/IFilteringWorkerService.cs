using HeavyStringFiltering.Business.DataTransferObjects;

namespace HeavyStringFiltering.Business.Services;

public interface IFilteringWorkerService
{
    Task ProcessAsync(UploadedRawStringDto uploadedRawString);
}