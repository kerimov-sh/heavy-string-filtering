using HeavyStringFiltering.Business.DataTransferObjects;
using HeavyStringFiltering.DataAccess;
using Microsoft.Extensions.Logging;

namespace HeavyStringFiltering.Business.Services.Implementations;

public class StringChunkService : IStringChunkService
{
    private readonly IChunkAssemblerService _chunkAssemblerService;
    private readonly IQueue<UploadedRawStringDto> _uploadedRawStringQueue;
    private readonly ILogger<StringChunkService> _logger;

    public StringChunkService(
        IChunkAssemblerService chunkAssemblerService,
        IQueue<UploadedRawStringDto> uploadedRawStringQueue,
        ILogger<StringChunkService> logger)
    {
        _chunkAssemblerService = chunkAssemblerService;
        _uploadedRawStringQueue = uploadedRawStringQueue;
        _logger = logger;
    }

    public Task<bool> AddChunkAsync(StringChunkDto stringChunk)
    {
        if (string.IsNullOrEmpty(stringChunk.UploadId))
        {
            _logger.LogError("Invalid upload id.");

            return Task.FromResult(false);
        }

        if (stringChunk.ChunkIndex < 0)
        {
            _logger.LogError("Invalid chunk index.");

            return Task.FromResult(false);
        }

        var operationResult = _chunkAssemblerService.AddChunk(stringChunk);

        if (stringChunk.IsLastChunk)
        {
            operationResult = _chunkAssemblerService.TryAssemble(
                stringChunk.UploadId,
                out var rawString);

            if (operationResult)
            {
                _uploadedRawStringQueue.Enqueue(new UploadedRawStringDto
                {
                    RawString = rawString!,
                    UploadId = stringChunk.UploadId
                });
            }
            else
            {
                _logger.LogError($"Chunk assembling failed. uploadId: {stringChunk.UploadId}");
            }
        }

        return Task.FromResult(operationResult);
    }
}