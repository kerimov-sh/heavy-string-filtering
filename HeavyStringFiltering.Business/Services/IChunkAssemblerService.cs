using HeavyStringFiltering.Business.DataTransferObjects;

namespace HeavyStringFiltering.Business.Services;

public interface IChunkAssemblerService
{
    bool AddChunk(StringChunkDto stringChunk);

    bool TryAssemble(string uploadId, out string? assembledData);
}