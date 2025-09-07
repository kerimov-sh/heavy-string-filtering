namespace HeavyStringFiltering.Business.Filtering;

public interface ISimilarityCalculator
{
    double CalculateSimilarity(string value1, string value2);
}