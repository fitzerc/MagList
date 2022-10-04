using MagList.Data.Models;

namespace MagList.Data.Read;

public class MockEntryReader : IEntryReader
{
    public IEnumerable<EntryModel> GetAll()
    {
        return new List<EntryModel>()
        {
            new EntryModel() { Name = "Milk", Description = "2%", Order = -1 },
            new EntryModel() { Name = "Ham", Description = "Black Forest", Order = -1 },
            new EntryModel() { Name = "Onion", Description = "Yellow", Order = -1 },
            new EntryModel() { Name = "Dish soap", Description = "Dawn", Order = -1 }
        };
    }

    public EntryModel Get(string name)
    {
        throw new NotImplementedException();
    }
}
