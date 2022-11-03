
using MagList.Data.Models;
using SQLite;

namespace MagList.Data.Read;

internal class SqliteTagReader : ITagReader
{
    private readonly SQLiteConnection _con;

    public SqliteTagReader(SQLiteConnection con)
    {
        _con = con;
        _con.CreateTable<TagModel>();
    }

    public IEnumerable<TagModel> GetTagsForEntry(int entryId)
    {
        return _con
            .Table<TagModel>()
            .Where(tag => tag.EntryId == entryId);
    }

    public IEnumerable<string> GetAllUniqueTagNames()
    {
        return _con
            .Table<TagModel>()
            .GroupBy(tag => tag.Name)
            .Select(tag => tag.Key);
    }

    public IEnumerable<TagModel> GetAllOfName(string name)
    {
        return _con
            .Table<TagModel>()
            .Where(tag => tag.Name == name);
    }
}