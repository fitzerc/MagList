using MagList.Data.Models;

namespace MagList.Data.Write;

public interface ITagWriter
{
    void Write(TagModel tag);
    void Delete(int tagId);
    void DeleteAll(IEnumerable<TagModel> tags);
}