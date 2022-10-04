using MagList.Data.Models;

namespace MagList.Data.Read;

public interface IEntryReader
{
    IEnumerable<EntryModel> GetAll();
    EntryModel Get(string name);
}
