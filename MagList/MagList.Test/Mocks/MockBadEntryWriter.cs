using MagList.Data.Models;
using MagList.Data.Write;

namespace MagList.Test.Mocks;

public class MockBadEntryWriter : IEntryWriter
{
    public void Write(EntryModel entry)
    {
        throw new NotImplementedException();
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
        throw new NotImplementedException();
    }

    public void Delete(int entryId)
    {
        throw new NotImplementedException();
    }

    public void Update(EntryModel entry)
    {
        throw new NotImplementedException();
    }
}