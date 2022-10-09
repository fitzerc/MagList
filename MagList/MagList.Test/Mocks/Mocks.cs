using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;

namespace MagList.Test.Mocks;

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