
using MagList.Data.Models;
using SQLite;

namespace MagList.Data.Write;

public class SqliteListWriter : IListWriter
{
    private readonly SQLiteConnection _con;

    public SqliteListWriter(SQLiteConnection con)
    {
        _con = con;
        _con.CreateTable<ListModel>();
    }

    public void Write(ListModel list)
    {
        _con.Insert(list);
    }

    public void Update(ListModel list)
    {
        _con.Update(list);
    }

    public void Delete(int listId)
    {
        _con.Delete<ListModel>(listId);
    }
}