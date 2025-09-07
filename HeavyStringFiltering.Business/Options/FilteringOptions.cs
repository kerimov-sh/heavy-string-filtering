using HeavyStringFiltering.Business.Filtering;

namespace HeavyStringFiltering.Business.Options;

public class FilteringOptions
{
    private readonly double _similarityThreshold;

    public ICollection<string>? FilterWords { get; init; }

    public SimilarityAlgorithm SimilarityAlgorithm { get; init; }

    public double SimilarityThreshold
    {
        get => _similarityThreshold;
        init => _similarityThreshold = Math.Clamp(value, 0.0, 1.0);
    }
}