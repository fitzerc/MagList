using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using MagList.Data.Read;
using System.Collections.ObjectModel;
using MagList.MainPage.EntriesListView;

namespace MagList.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IEntryReader _entryReader;
    private readonly IListReader _listReader;

    private List<ListModel> _lists;
    private ListModel _currentList;

    public EventHandler<EntryViewModel> EntryAdded;
    public EventHandler<ListModel> ListChanged;

    public MainPageViewModel(
        IEntryReader entryReader,
        IListReader listReader,
        EntriesListViewModel entriesListVm)
    {
        _entryReader = entryReader ?? throw new ArgumentNullException(nameof(entryReader));
        _listReader = listReader ?? throw new ArgumentNullException(nameof(listReader));
        entryListVm = entriesListVm;

        try
        {
            _lists = _listReader.GetAll().ToList();

            _currentList = _lists.FirstOrDefault();

            var entries = _entryReader.GetAllInList(_currentList.Id);

            //TODO: move mapping out of VM
            foreach (var entry in entries)
            {
                EntryList.Add(entry.ToEntryViewModel());
            }

            entriesListVm.EntryList = EntryList;
            entriesListVm.CurrentList = _currentList;
        }
        catch (Exception e)
        {
            throw new MainPageViewModelInitException("Unable to initialize data", e);
        }
    }

    [ObservableProperty]
    ObservableCollection<EntryViewModel> entryList = new ObservableCollection<EntryViewModel>();

    [ObservableProperty]
    string newEntryName = "";

    [ObservableProperty]
    EntriesListViewModel entryListVm;

    [RelayCommand]
    void AddClicked()
    {
        var newEntry = new EntryViewModel()
        {
            Name = NewEntryName,
            ListId = _currentList.Id,
            Description = $"Description for {NewEntryName}",
            Order = EntryList.Count + 1,
        };

        EntryAdded.Invoke(this, newEntry);
        EntryList.Add(newEntry);

        NewEntryName = "";
    }
}

public class MainPageViewModelInitException : Exception
{
    public MainPageViewModelInitException(string message, Exception innerException) : base(message, innerException) {}
}
