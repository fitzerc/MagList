using MagList.Data.Models;
using SQLite;

namespace MagList.Data.Write;

public class SqliteTagWriter : ITagWriter
{
    private readonly SQLiteConnection _con;

    public SqliteTagWriter(SQLiteConnection con)
    {
        _con = con;
        _con.CreateTable<TagModel>();
    }

    public void Write(TagModel tag)
    {
        _con.Insert(tag);
    }

    public void Delete(int tagId)
    {
        _con.Delete<TagModel>(tagId);
    }

    public void DeleteAll(IEnumerable<TagModel> tags)
    {
        foreach (var tag in tags)
        {
            Delete(tag.Id);
        }
    }
}