using HeavyStringFiltering.Business.Filtering;
using HeavyStringFiltering.Business.Filtering.Implementations;
using HeavyStringFiltering.Business.Options;
using HeavyStringFiltering.Business.Services;
using HeavyStringFiltering.Business.Services.Implementations;

namespace HeavyStringFiltering.Business.Tests
{
    [TestClass]
    public sealed class FilteringLogicTest
    {
        private IFilterService _filterService;

        [TestInitialize]
        public void Initialize()
        {
            var filteringOptions = new FilteringOptions
            {
                SimilarityThreshold = 0.75,
                SimilarityAlgorithm = SimilarityAlgorithm.Levenshtein,
                FilterWords = [
                    "bad",
                    "spam",
                    "worst"
                    ]
            };
            var options = Microsoft.Extensions.Options.Options.Create(filteringOptions);
            var similarityCalculatorFactory = new DefaultSimilarityCalculatorFactory(options);
            _filterService = new FilterService(options, similarityCalculatorFactory);
        }

        [TestMethod]
        public void FilterBadWords()
        {
            var input = "This text contains Badd, spam words";
            var expected = "This text contains ,  words";

            var result = _filterService.Filter(input);

            Assert.AreEqual(expected, result);
        }
    }
}
