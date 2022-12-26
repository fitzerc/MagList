using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MagList.MainPage.EntriesListView;
using MagList.State;

namespace MagList.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    public EventHandler<EntryViewModel> EntryAdded;

    private readonly AppState _appState;

    public MainPageViewModel(EntriesListViewModel entriesListVm, AppState appState)
    {
        _appState = appState;

        //TODO: move to use state
        entryListVm = entriesListVm;

        try
        {
            var entries = _appState.CurrentList.Entries;

            EntryList = _appState.CurrentList.EntryVms;

            entriesListVm.EntryList = _appState.CurrentList.EntryVms;
            entriesListVm.CurrentList = _appState.CurrentList;
        }
        catch (Exception e)
        {
            throw new MainPageViewModelInitException("Unable to initialize data", e);
        }
    }

    [ObservableProperty]
    ObservableCollection<EntryViewModel> entryList;

    [ObservableProperty]
    string newEntryName;

    [ObservableProperty]
    EntriesListViewModel entryListVm;

    [RelayCommand]
    void AddClicked()
    {
        var newEntry = new EntryViewModel()
        {
            Name = NewEntryName,
            ListId = _appState.CurrentList.List.Id,
            Description = $"Description for {NewEntryName}",
            Order = EntryList.Count + 1,
        };

        EntryAdded.Invoke(this, newEntry);

        //TODO: how to update this based on observables rather than manually?
        EntryListVm.EntryList = _appState.CurrentList.EntryVms;

        NewEntryName = "";
    }
}

public class MainPageViewModelInitException : Exception
{
    public MainPageViewModelInitException(string message, Exception innerException) : base(message, innerException) {}
}
