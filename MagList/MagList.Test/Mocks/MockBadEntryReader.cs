using MagList.Data.Models;
using MagList.Data.Read;

namespace MagList.Test.Mocks;

public class MockBadEntryReader : IEntryReader
{
    public IEnumerable<EntryModel> GetAllInList(int listId)
    {
        throw new NotImplementedException();
    }

    public EntryModel Get(string name)
    {
        throw new NotImplementedException();
    }
}