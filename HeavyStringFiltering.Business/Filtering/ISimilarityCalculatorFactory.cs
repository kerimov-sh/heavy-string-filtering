namespace HeavyStringFiltering.Business.Filtering;

public interface ISimilarityCalculatorFactory
{
    ISimilarityCalculator CreateSimilarityCalculator();
}