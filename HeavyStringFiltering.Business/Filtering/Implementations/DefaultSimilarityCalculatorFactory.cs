using HeavyStringFiltering.Business.Options;
using Microsoft.Extensions.Options;

namespace HeavyStringFiltering.Business.Filtering.Implementations;

public class DefaultSimilarityCalculatorFactory : ISimilarityCalculatorFactory
{
    private readonly IOptions<FilteringOptions> _filteringOptions;

    public DefaultSimilarityCalculatorFactory(IOptions<FilteringOptions> filteringOptions)
    {
        _filteringOptions = filteringOptions;
    }

    public ISimilarityCalculator CreateSimilarityCalculator()
    {
        switch (_filteringOptions.Value.SimilarityAlgorithm)
        {
            case SimilarityAlgorithm.Levenshtein:
                return new LevenshteinDistanceSimilarityCalculator();
            default:
                throw new InvalidOperationException("Algorithm is not defined.");
        }
    }
}