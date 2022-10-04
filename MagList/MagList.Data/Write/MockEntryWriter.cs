using MagList.Data.Models;

namespace MagList.Data.Write;

public class MockEntryWriter : IEntryWriter
{
    public void Delete(EntryModel entry)
    {
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
    }

    public void Write(EntryModel entry)
    {
    }
}
