using MagList.Data.Models;
using MagList.Data.Read;

namespace MagList.Test.Mocks;

internal class MockTagReader : ITagReader
{
    public List<TagModel> Tags = new List<TagModel>
    {
        new TagModel
        {
            Id = 1,
            EntryId = 1,
            Name = "test1"
        },
        new TagModel
        {
            Id = 2,
            EntryId = 1,
            Name = "test2"
        },
    };

    public IEnumerable<TagModel> GetTagsForEntry(int entryId)
    {
        return Tags;
    }

    public IEnumerable<string> GetAllUniqueTagNames()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TagModel> GetAllOfName(string name)
    {
        throw new NotImplementedException();
    }
}