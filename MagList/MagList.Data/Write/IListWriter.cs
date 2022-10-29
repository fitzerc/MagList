using MagList.Data.Models;

namespace MagList.Data.Write;

public interface IListWriter
{
    void Write(ListModel list);
    void Update(ListModel list);
    void Delete(int listId);
}