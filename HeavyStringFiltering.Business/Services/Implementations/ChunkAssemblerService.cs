using HeavyStringFiltering.Business.DataTransferObjects;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text;

namespace HeavyStringFiltering.Business.Services.Implementations;

public class ChunkAssemblerService : IChunkAssemblerService
{
    private readonly ILogger<ChunkAssemblerService> _logger;
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<int, string>> _uploads = new();

    public ChunkAssemblerService(ILogger<ChunkAssemblerService> logger)
    {
        _logger = logger;
    }

    public bool AddChunk(StringChunkDto stringChunk)
    {
        var chunks = _uploads.GetOrAdd(
            stringChunk.UploadId,
            _ => new ConcurrentDictionary<int, string>());
        var isAdded = chunks.TryAdd(stringChunk.ChunkIndex, stringChunk.Data);
        if (!isAdded)
        {
            _logger.LogError(
                $"Add chunk operation failed. uploadId: {stringChunk.UploadId}, chunk index: {stringChunk.ChunkIndex}");
        }

        return isAdded;
    }

    public bool TryAssemble(string uploadId, out string? assembledData)
    {
        assembledData = null;

        if (!_uploads.TryRemove(uploadId, out var chunks))
        {
            _logger.LogError($"Invalid uploadId. uploadId: {uploadId}");

            return false;
        }

        var ordered = chunks
            .OrderBy(x => x.Key)
            .Select(x => x.Value);
        var stringBuilder = new StringBuilder(chunks.Values.Sum(x => x.Length));
        foreach (var data in ordered)
        {
            stringBuilder.Append(data);
        }

        assembledData = stringBuilder.ToString();

        return true;
    }
}