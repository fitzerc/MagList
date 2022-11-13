using MagList.Data.Models;
using MagList.Data.Read;

namespace MagList.Test.Mocks;

public class MockBadListReader : IListReader
{
    public IEnumerable<ListModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public ListModel Get(int id)
    {
        throw new NotImplementedException();
    }

    public ListModel Get(string name)
    {
        throw new NotImplementedException();
    }
}