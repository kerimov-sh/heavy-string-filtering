namespace HeavyStringFiltering.Business.DataTransferObjects;

public class StringChunkDto
{
    private readonly string _uploadId;

    public string UploadId
    {
        get => _uploadId;
        init => _uploadId = value.Trim();
    }

    public int ChunkIndex { get; init; }

    public string Data { get; init; }

    public bool IsLastChunk { get; init; }
}