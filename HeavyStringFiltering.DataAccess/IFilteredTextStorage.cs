namespace HeavyStringFiltering.DataAccess;

public interface IFilteredTextStorage
{
    void Save(string uploadId, string filteredText);
}