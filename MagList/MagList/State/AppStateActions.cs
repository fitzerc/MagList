using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using System.Collections.ObjectModel;

namespace MagList.State;

public class AppStateActions
{
    private readonly AppState _appState;
    private readonly IEntryWriter _entryWriter;
    private readonly IEntryReader _entryReader;
    private readonly IListWriter _listWriter;
    private readonly IListReader _listReader;
    private readonly ITagWriter _tagWriter;
    private readonly ITagReader _tagReader;

    public AppStateActions(
        AppState appState,
        IEntryWriter entryWriter,
        IEntryReader entryReader,
        IListWriter listWriter,
        IListReader listReader,
        ITagWriter tagWriter,
        ITagReader tagReader)
    {
        _appState = appState;
        _entryWriter = entryWriter;
        _entryReader = entryReader;
        _listWriter = listWriter;
        _listReader = listReader;
        _tagWriter = tagWriter;
        _tagReader = tagReader;
    }

    public void AddEntry(EntryModel entry)
    {
        _entryWriter.Write(entry);
        _appState.CurrentList.Entries =
            new ObservableCollection<EntryModel>(_entryReader.GetAllInList(_appState.CurrentList.List.Id));
    }
}
