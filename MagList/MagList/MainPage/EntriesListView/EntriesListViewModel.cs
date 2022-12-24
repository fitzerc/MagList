using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.EntryDetailPage;
using System.Collections.ObjectModel;
using MagList.State;

namespace MagList.MainPage.EntriesListView;

public partial class EntriesListViewModel : ObservableObject
{
    public EventHandler<IEnumerable<EntryViewModel>> SortOrderChanged;
    public EventHandler<EntryViewModel> EntryDeleted;

    private Func<string, Dictionary<string, object>, Task> _onEntryTapped;

    [ObservableProperty]
    ListState _currentList;

    [ObservableProperty]
    AppState _appState;

    [ObservableProperty]
    ObservableCollection<EntryViewModel> entryList;

    public EntriesListViewModel(
        Func<string, Dictionary<string, object>, Task> onEntryTapped,
        EventHandler<IEnumerable<EntryViewModel>> sortOrderChanged,
        EventHandler<EntryViewModel> entryDeleted,
        AppState appState)
    {
        _onEntryTapped = onEntryTapped;
        SortOrderChanged = sortOrderChanged;
        EntryDeleted = entryDeleted;

        _appState = appState;
        _currentList = _appState.CurrentList;
        EntryList = _currentList.EntryVms;
    }

    [RelayCommand]
    async Task EntryTapped(EntryViewModel entry)
    {
        _appState.CurrentEntry = new (){ Entry = entry.ToEntryModel() };

        var navParams = new Dictionary<string, object>
        {
            {EntryDetailViewModel.LIST_NAME_PARAM_NAME, _currentList.List.Name},
            {nameof(EntryViewModel), _appState.CurrentEntry}
        };

        await _onEntryTapped.Invoke(nameof(EntryDetailView), navParams);
    }

    [RelayCommand]
    void ItemDragged(EntryViewModel entry)
    {
        EntryList
            .First(x => x.Name == entry.Name && x.Description == entry.Description)
            .IsBeingDragged = true;
    }

    [RelayCommand]
    void ItemDraggedOver(EntryViewModel entry)
    {
        EntryList
            .First(x => x.Name == entry.Name && x.Description == entry.Description)
            .IsBeingDraggedOver = true;
    }

    [RelayCommand]
    void ItemDragLeave(EntryViewModel entry)
    {
        EntryList
            .First(x => x.Name == entry.Name && x.Description == entry.Description)
            .IsBeingDraggedOver = false;
    }

    [RelayCommand]
    void ItemDropped(EntryViewModel item)
    {
        var itemToMove = EntryList.First(i => i.IsBeingDragged);
        var itemToInsertBefore = item;
        if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
        {
            return;
        }

        var insertAtIndex = EntryList.IndexOf(itemToInsertBefore);
        EntryList.Remove(itemToMove);
        EntryList.Insert(insertAtIndex, itemToMove);
        itemToMove.IsBeingDragged = false;
        itemToInsertBefore.IsBeingDraggedOver = false;

        ReorderAndWriteList();
    }

    private void ReorderAndWriteList()
    {
        UpdateListEntryOrders();
    }

    private void UpdateListEntryOrders()
    {
        for (var i = 0; i < EntryList.Count; i++)
        {
            EntryList[i].Order = i + 1;
        }

        SortOrderChanged.Invoke(this, EntryList);
    }

    [RelayCommand]
    void DeleteClicked(EntryViewModel entry)
    {
        EntryDeleted.Invoke(this, entry);
        //TODO: should move to a single source and re-query after the write
        EntryList.Remove(entry);
    }
}