using HeavyStringFiltering.Business.DataTransferObjects;

namespace HeavyStringFiltering.Business.Services;

public interface IStringChunkService
{
    Task<bool> AddChunkAsync(StringChunkDto stringChunk);
}