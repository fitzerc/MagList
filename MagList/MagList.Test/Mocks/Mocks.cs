using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;

namespace MagList.Test.Mocks;

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

public class MockBadEntryReader : IEntryReader
{
    public IEnumerable<EntryModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public EntryModel Get(string name)
    {
        throw new NotImplementedException();
    }
}

public class MockEntryWriter : IEntryWriter
{
    public int LastWriteId;
    public int LastDeleteId;

    public void Delete(int entryId)
    {
    }

    public void Update(EntryModel entry)
    {
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
    }

    public void Write(EntryModel entry)
    {
    }
}

public class MockBadEntryWriter : IEntryWriter
{
    public void Write(EntryModel entry)
    {
        throw new NotImplementedException();
    }

    public void UpdateAll(IEnumerable<EntryModel> entries)
    {
        throw new NotImplementedException();
    }

    public void Delete(int entryId)
    {
        throw new NotImplementedException();
    }

    public void Update(EntryModel entry)
    {
        throw new NotImplementedException();
    }
}