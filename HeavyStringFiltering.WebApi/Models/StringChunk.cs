namespace HeavyStringFiltering.WebApi.Models;

public class StringChunk
{
    public string UploadId { get; init; }

    public int ChunkIndex { get; init; }

    public string Data { get; init; }

    public bool IsLastChunk { get; init; }
}