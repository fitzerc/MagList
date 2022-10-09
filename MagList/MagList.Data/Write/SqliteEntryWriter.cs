using MagList.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagList.Data.Write;

public class SqliteEntryWriter : IEntryWriter
{
    private readonly SQLiteConnection _con;

    public SqliteEntryWriter(SQLiteConnection con)
    {
        _con = con;
        con.CreateTable<EntryModel>();
    }

    public void Delete(int entryId)
    {
        _con.Delete<EntryModel>(entryId);
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
        _con.UpdateAll(entries);
    }

    public void Write(EntryModel entry)
    {
        _con.Insert(entry);
    }

    public void Update(EntryModel entry)
    {
        _con.Update(entry);
    }
}
