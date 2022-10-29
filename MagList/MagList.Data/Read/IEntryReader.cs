using MagList.Data.Models;

namespace MagList.Data.Read;

public interface IEntryReader
{
    IEnumerable<EntryModel> GetAllInList(int listId);
    EntryModel Get(string name);
}
