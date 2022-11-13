using MagList.Data.Models;
using MagList.Data.Write;

namespace MagList.Test.Mocks;

public class MockEntryWriter : IEntryWriter
{
    public int LastWriteId;
    public int LastDeleteId;
    public EntryModel LastUpdateModel;

    public void Delete(int entryId)
    {
    }

    public void Update(EntryModel entry)
    {
        LastUpdateModel = entry;
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
    }

    public void Write(EntryModel entry)
    {
    }
}