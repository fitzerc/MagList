using SQLite;

namespace MagList.Data.Models;

[Table("tag")]
public class TagModel
{
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }

    [Column("entryId")]
    public int EntryId { get; set; }

    [Column("name")]
    public string Name { get; set; }
}