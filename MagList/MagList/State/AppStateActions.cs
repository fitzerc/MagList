using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using MagList.MainPage;
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
        RefreshCurrentEntries();
    }

    public void UpdateEntry(EntryModel entry)
    {
        _entryWriter.Update(entry);
        RefreshCurrentEntries();
    }

    public void UpdateAllEntries(IEnumerable<EntryModel> entriesList)
    {
        _entryWriter.UpdateAll(entriesList);
        RefreshCurrentEntries();
    }

    public void DeleteEntry(EntryModel entry)
    {
        _entryWriter.Delete(entry.Id);
        RefreshCurrentEntries();
    }

    public void AddTag(TagModel tag)
    {
        _tagWriter.Write(tag);
        RefreshTags();
    }

    public void DeleteTag(TagModel tag)
    {
        _tagWriter.Delete(tag.Id);
        RefreshTags();
    }

    public void SetCurrentEntryDetailState(EntryModel entryVm)
    {
        _appState.CurrentEntry = new EntryState() { Entry = entryVm };
        _appState.CurrentEntryDetailState = new CurrentEntryDetailState
        {
            EntryViewModel = entryVm.ToEntryViewModel(),
        };

        RefreshTags();
    }

    private void RefreshTags()
    {
        _appState.CurrentEntryDetailState.Tags =
            new ObservableCollection<TagModel>(
                _tagReader.GetTagsForEntry(_appState.CurrentEntryDetailState.EntryViewModel.Id));
    }


    private void RefreshCurrentEntries()
    {
        _appState.CurrentList.Entries =
            new ObservableCollection<EntryModel>(_entryReader.GetAllInList(_appState.CurrentList.List.Id));
    }
}
