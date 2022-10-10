using MagList.Data.Models;
using SQLite;

namespace MagList.Data.Read;

//TODO: make a base that takes a sql connection?
public class SqliteEntryReader : IEntryReader
{
    private readonly SQLiteConnection _con;

    public SqliteEntryReader(SQLiteConnection con)
    {
        _con = con;
        con.CreateTable<EntryModel>();
    }

    public EntryModel Get(string name)
    {
        var record = _con
            .Table<EntryModel>()
            .FirstOrDefault(entry => entry.Name == name);

        return record;
    }

    public IEnumerable<EntryModel> GetAll()
    {
        return _con.Table<EntryModel>();
    }
}
