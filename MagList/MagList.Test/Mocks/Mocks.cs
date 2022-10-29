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

public class MockBadEntryReader : IEntryReader
{
    public IEnumerable<EntryModel> GetAllInList(int listId)
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

public class MockListReader : IListReader
{
    public ListModel CurrentList = new () {Id = 1, Name = "Default"};

    public IEnumerable<ListModel> GetAll()
    {
        return new List<ListModel>() {CurrentList};
    }

    public ListModel Get(int id)
    {
        return CurrentList;
    }

    public ListModel Get(string name)
    {
        return CurrentList;
    }
}

public class MockBadListReader : IListReader
{
    public IEnumerable<ListModel> GetAll()
    {
        throw new NotImplementedException();
    }

    public ListModel Get(int id)
    {
        throw new NotImplementedException();
    }

    public ListModel Get(string name)
    {
        throw new NotImplementedException();
    }
}

public class MockListWriter : IListWriter
{
    public void Write(ListModel list)
    {
        throw new NotImplementedException();
    }

    public void Update(ListModel list)
    {
        throw new NotImplementedException();
    }

    public void Delete(int listId)
    {
        throw new NotImplementedException();
    }
}

public class MockBadListWriter : IListWriter
{
    public void Write(ListModel list)
    {
        throw new NotImplementedException();
    }

    public void Update(ListModel list)
    {
        throw new NotImplementedException();
    }

    public void Delete(int listId)
    {
        throw new NotImplementedException();
    }
}