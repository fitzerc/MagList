using MagList.Data.Models;
using MagList.Data.Read;

namespace MagList.Test.Mocks;

public class MockListReader : IListReader
{
    public ListModel CurrentList = new () {Id = 1, Name = "Default"};

    public IEnumerable<ListModel> GetAll()
    {
        return new List<ListModel>() {CurrentList};
    }

    public ListModel Get(int id)
    {
        return CurrentList;
    }

    public ListModel Get(string name)
    {
        return CurrentList;
    }
}