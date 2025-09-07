namespace HeavyStringFiltering.Business.Filtering.Implementations;

public class LevenshteinDistanceSimilarityCalculator : ISimilarityCalculator
{
    public double CalculateSimilarity(string value1, string value2)
    {
        if ((value1.Length == 0 && value2.Length == 0) ||
            value1.Equals(value2, StringComparison.OrdinalIgnoreCase))
        {
            return 1.0;
        }

        var levenshteinDistance = CalculateLevenshteinDistance(value1, value2);
        var similarity = 1 - (double)levenshteinDistance / Math.Max(value1.Length, value2.Length);

        return similarity;
    }

    private static int CalculateLevenshteinDistance(string value1, string value2)
    {
        var rowCount = value1.Length + 1;
        var columnCount = value2.Length + 1;
        var distanceMatrix = new int[rowCount, columnCount];

        for (var i = 0; i < distanceMatrix.GetLength(1); ++i)
        {
            distanceMatrix[0, i] = i;
        }

        for (var i = 0; i < distanceMatrix.GetLength(0); ++i)
        {
            distanceMatrix[i, 0] = i;
        }

        for (var r = 1; r < distanceMatrix.GetLength(0); ++r)
        {
            for (var c = 1; c < distanceMatrix.GetLength(1); ++c)
            {
                var deletionFromStr1 = distanceMatrix[r - 1, c] + 1;
                var insertionIntoStr2 = distanceMatrix[r, c - 1] + 1;
                var substitutionCost = (char.ToLower(value1[r - 1]) == char.ToLower(value2[c - 1]) ? 0 : 1) +
                                       distanceMatrix[r - 1, c - 1];
                distanceMatrix[r, c] = Math.Min(
                    deletionFromStr1,
                    Math.Min(insertionIntoStr2, substitutionCost));
            }
        }

        return distanceMatrix[rowCount - 1, columnCount - 1];
    }
}