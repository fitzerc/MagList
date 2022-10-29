using SQLite;

namespace MagList.Data.Models;

[Table("list")]
public class ListModel
{
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; }
}