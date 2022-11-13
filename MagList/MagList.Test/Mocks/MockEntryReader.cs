using MagList.Data.Models;
using MagList.Data.Read;

namespace MagList.Test.Mocks;

public class MockEntryReader : IEntryReader
{
    public ListModel CurrentList = new () {Id = 1, Name = "Default"};
    public List<EntryModel> _entries = new List<EntryModel>()
    {
        new EntryModel() {Id = 1, ListId = 1, Name = "Milk", Description = "2%", Order = -1},
        new EntryModel() {Id = 2, ListId = 1, Name = "Ham", Description = "Black Forest", Order = -1},
        new EntryModel() {Id = 3, ListId = 1, Name = "Onion", Description = "Yellow", Order = -1},
        new EntryModel() {Id = 4, ListId = 1, Name = "Dish soap", Description = "Dawn", Order = -1}
    };

    public IEnumerable<EntryModel> GetAllInList(int listId) => _entries;

    public EntryModel Get(string name) => _entries.FirstOrDefault(x => x.Name == name);
}