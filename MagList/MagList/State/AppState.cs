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
}

public partial class ListState : ObservableObject
{
    [ObservableProperty]
    ListModel _list;

    [ObservableProperty]
    ObservableCollection<EntryModel> _entries;

    public ObservableCollection<EntryViewModel> EntryVms =>
        new ObservableCollection<EntryViewModel>(
            _entries.Select(x => x.ToEntryViewModel()).ToList()
            );
}

public partial class EntryState : ObservableObject
{
    [ObservableProperty]
    EntryModel _entry = new ();

    [ObservableProperty]
    string _newEntryName = "";

    EntryViewModel EntryVm => _entry.ToEntryViewModel();
}
