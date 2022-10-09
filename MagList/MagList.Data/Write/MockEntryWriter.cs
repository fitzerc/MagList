using MagList.Data.Models;

namespace MagList.Data.Write;

public class MockEntryWriter : IEntryWriter
{
    public int LastWriteId;
    public int LastDeleteId;

    public void Delete(int entryId)
    {
    }

    public void Update(EntryModel entry)
    {
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
    }

    public void Write(EntryModel entry)
    {
    }
}
