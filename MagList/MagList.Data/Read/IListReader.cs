using MagList.Data.Models;

namespace MagList.Data.Read;

public interface IListReader
{
    IEnumerable<ListModel> GetAll();
    ListModel Get(int id);
    ListModel Get(string name);
}