using MagList.Data.Models;
using SQLite;

namespace MagList.Data.Read;

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
        var record = _con.Table<EntryModel>()
            .Where(entry => entry.Name == name)
            .FirstOrDefault();

        return record;
    }

    public IEnumerable<EntryModel> GetAll()
    {
        return _con.Table<EntryModel>();
    }
}
