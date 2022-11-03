using MagList.Data.Models;

namespace MagList.Data.Read;

public interface ITagReader
{
    IEnumerable<TagModel> GetTagsForEntry(int entryId);
    IEnumerable<string> GetAllUniqueTagNames();
    IEnumerable<TagModel> GetAllOfName(string name);
}