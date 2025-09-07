using HeavyStringFiltering.Business.Filtering;
using HeavyStringFiltering.Business.Options;
using Microsoft.Extensions.Options;
using System.Text;

namespace HeavyStringFiltering.Business.Services.Implementations;

public class FilterService : IFilterService
{
    private readonly IOptions<FilteringOptions> _filteringOptions;
    private readonly ISimilarityCalculator _similarityCalculator;

    public FilterService(
        IOptions<FilteringOptions> filteringOptions,
        ISimilarityCalculatorFactory similarityCalculatorFactory)
    {
        _filteringOptions = filteringOptions;
        _similarityCalculator = similarityCalculatorFactory.CreateSimilarityCalculator();
    }

    public string Filter(string input)
    {
        var filterWords = _filteringOptions.Value.FilterWords?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        if (filterWords is not { Count: > 0 })
        {
            return input;
        }

        var stringBuilder = new StringBuilder(input.Length);

        foreach (var readWordResult in ReadWords(input))
        {
            if (!readWordResult.IsWord)
            {
                stringBuilder.Append(readWordResult.Value);
                continue;
            }

            var appendWord = true;

            foreach (var filterWord in filterWords)
            {
                var similarity = _similarityCalculator.CalculateSimilarity(readWordResult.Value, filterWord);
                if (similarity >= _filteringOptions.Value.SimilarityThreshold)
                {
                    appendWord = false;
                    break;
                }
            }

            if (appendWord)
            {
                stringBuilder.Append(readWordResult.Value);
            }
        }

        return stringBuilder.ToString();
    }

    private static IEnumerable<(string Value, bool IsWord)> ReadWords(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            yield break;
        }

        var stringBuilder = new StringBuilder();
        var inWord = false;

        foreach (var ch in input)
        {
            if (char.IsLetter(ch))
            {
                if (!inWord && stringBuilder.Length > 0)
                {
                    yield return (stringBuilder.ToString(), false);

                    stringBuilder.Clear();
                }

                stringBuilder.Append(ch);
                inWord = true;
            }
            else
            {
                if (inWord && stringBuilder.Length > 0)
                {
                    yield return (stringBuilder.ToString(), true);

                    stringBuilder.Clear();
                }

                stringBuilder.Append(ch);
                inWord = false;
            }
        }

        if (stringBuilder.Length > 0)
        {
            yield return (stringBuilder.ToString(), inWord);
        }
    }
}