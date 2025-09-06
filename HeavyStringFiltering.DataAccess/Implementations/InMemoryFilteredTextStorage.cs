using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace HeavyStringFiltering.DataAccess.Implementations;

public class InMemoryFilteredTextStorage : IFilteredTextStorage
{
    private readonly ILogger<InMemoryFilteredTextStorage> _logger;
    private readonly ConcurrentDictionary<string, string> _textStorage = new();

    public InMemoryFilteredTextStorage(ILogger<InMemoryFilteredTextStorage> logger)
    {
        _logger = logger;
    }

    public void Save(string uploadId, string filteredText)
    {
        var isAdded = _textStorage.TryAdd(uploadId, filteredText);
        if (!isAdded)
        {
            _logger.LogError($"Save operation failed. uploadId: {uploadId}");
        }
    }
}