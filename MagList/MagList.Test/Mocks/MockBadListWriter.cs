using MagList.Data.Models;
using MagList.Data.Write;

namespace MagList.Test.Mocks;

public class MockBadListWriter : IListWriter
{
    public void Write(ListModel list)
    {
        throw new NotImplementedException();
    }

    public void Update(ListModel list)
    {
        throw new NotImplementedException();
    }

    public void Delete(int listId)
    {
        throw new NotImplementedException();
    }
}