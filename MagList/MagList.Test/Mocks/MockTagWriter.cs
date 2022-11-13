using MagList.Data.Models;
using MagList.Data.Write;

namespace MagList.Test.Mocks;

internal class MockTagWriter : ITagWriter
{
    public TagModel LastWrite = null;
    public int LastDelete = -1;
    public int CountLastDeleted = -1;

    public void Write(TagModel tag)
    {
        LastWrite = tag;
    }

    public void Delete(int tagId)
    {
        LastDelete = tagId;
    }

    public void DeleteAll(IEnumerable<TagModel> tags)
    {
        CountLastDeleted = tags.Count();
    }
}