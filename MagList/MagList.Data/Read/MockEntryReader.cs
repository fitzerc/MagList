using MagList.Data.Models;

namespace MagList.Data.Read;

public class MockEntryReader : IEntryReader
{
    public List<EntryModel> _entries = new List<EntryModel>()
    {
        new EntryModel() {Id = 1, Name = "Milk", Description = "2%", Order = -1},
        new EntryModel() {Id = 2, Name = "Ham", Description = "Black Forest", Order = -1},
        new EntryModel() {Id = 3, Name = "Onion", Description = "Yellow", Order = -1},
        new EntryModel() {Id = 4, Name = "Dish soap", Description = "Dawn", Order = -1}
    };

    public IEnumerable<EntryModel> GetAll() => _entries;

    public EntryModel Get(string name) => _entries.FirstOrDefault(x => x.Name == name);
}
