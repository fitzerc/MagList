using MagList.Data.Models;

namespace MagList.Data.Write;

public interface IEntryWriter
{
    void Write(EntryModel entry);
    void UpdateAll(IEnumerable<EntryModel> entries);
    void Delete(int entryId);
    void Update(EntryModel entry);
}
