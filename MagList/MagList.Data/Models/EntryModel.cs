using SQLite;

namespace MagList.Data.Models;

[Table("entry")]
public class EntryModel
{
    [PrimaryKey, AutoIncrement, Column("id")]
    public int Id { get; set; }

    [Column("listId")]
    public int ListId { get; set; }

    [MaxLength(30)]
    public string Name { get; set;}

    [MaxLength(100)]
    public string Description { get; set;}

    public int Order { get; set;}

    public bool Equals(EntryModel obj)
    {
        return obj.Id == Id && obj.Name == Name && obj.Description == Description && obj.Order == Order;
    }
}
