using MagList.Data.Models;
using SQLite;

namespace MagList.Data.Read;

public class SqliteListReader : IListReader
{
    private readonly SQLiteConnection _con;

    public SqliteListReader(SQLiteConnection con)
    {
        _con = con;
        _con.CreateTable<ListModel>();
    }

    public IEnumerable<ListModel> GetAll()
    {
        return _con.Table<ListModel>();
    }

    public ListModel Get(int id)
    {
        var record = _con
            .Table<ListModel>()
            .FirstOrDefault(list => list.Id == id);

        return record;
    }

    public ListModel Get(string name)
    {
        var record = _con
            .Table<ListModel>()
            .FirstOrDefault(list => list.Name == name);

        return record;
    }
}