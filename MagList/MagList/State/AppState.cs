using CommunityToolkit.Mvvm.ComponentModel;
using MagList.Data.Models;
using MagList.MainPage;
using System.Collections.ObjectModel;

namespace MagList.State;

public partial class AppState : ObservableObject
{
    [ObservableProperty]
    ListState _currentList;

    [ObservableProperty]
    ObservableCollection<ListModel> _lists;

    [ObservableProperty]
    EntryState _currentEntry;

    [ObservableProperty]
    CurrentEntryDetailState _currentEntryDetailState;
}

public partial class ListState : ObservableObject
{
    [ObservableProperty]
    ListModel _list;

    [ObservableProperty]
    ObservableCollection<EntryModel> _entries;

    public void SetEntries(ObservableCollection<EntryModel> entries)
    {
        var vmCollection = entries.Select(entryModel => entryModel.ToEntryViewModel()).ToList();

        _entryVms = new ObservableCollection<EntryViewModel>(vmCollection);
    }

    [ObservableProperty]
    ObservableCollection<EntryViewModel> _entryVms;
}

public partial class EntryState : ObservableObject
{
    [ObservableProperty]
    EntryModel _entry = new ();

    [ObservableProperty]
    string _newEntryName = "";

    public EntryViewModel EntryVm => _entry.ToEntryViewModel();
}

public partial class CurrentEntryDetailState : ObservableObject
{
    [ObservableProperty]
    EntryViewModel _entryViewModel;

    [ObservableProperty]
    ObservableCollection<TagModel> _tags = new ObservableCollection<TagModel>();
}
