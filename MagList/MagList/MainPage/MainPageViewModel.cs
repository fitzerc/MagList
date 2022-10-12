using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using System.Collections.ObjectModel;
using MagList.EntryDetailPage;

namespace MagList.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IEntryReader _entryReader;
    private readonly IEntryWriter _entryWriter;

    public MainPageViewModel(IEntryReader entryReader, IEntryWriter entryWriter)
    {
        _entryReader = entryReader ?? throw new ArgumentNullException(nameof(entryReader));
        _entryWriter = entryWriter ?? throw new ArgumentNullException(nameof(entryWriter));

        try
        {
            var entries = _entryReader.GetAll();

            foreach (var entry in entries)
            {
                EntryList.Add(EntryViewModel.FromEntryModel(entry));
            }
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

    [RelayCommand]
    async Task EntryTapped(EntryViewModel entry)
    {
        var navParams = new Dictionary<string, object>
        {
            {nameof(EntryViewModel), entry}
        };

        await Shell.Current.GoToAsync(nameof(EntryDetailView), navParams);
    }

    [RelayCommand]
    void AddClicked()
    {
        var newEntry = new EntryModel()
        {
            Name = NewEntryName,
            Description = $"Description for {NewEntryName}",
            Order = EntryList.Count + 1
        };

        _entryWriter?.Write(newEntry);
        EntryList.Add(EntryViewModel.FromEntryModel(newEntry));

        NewEntryName = "";
    }

    [RelayCommand]
    void DeleteClicked(EntryViewModel entry)
    {
        _entryWriter?.Delete(entry.Id);
        EntryList.Remove(entry);
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

        _entryWriter.UpdateAll(MapToEntryModelCollection(EntryList));
    }

    private IEnumerable<EntryModel> MapToEntryModelCollection(ObservableCollection<EntryViewModel> entryList)
    {
        var modelList = new List<EntryModel>();

        foreach (var entry in entryList)
        {
            modelList.Add(EntryViewModel.ToEntryModel(entry));
        }

        return modelList;
    }
}

public class MainPageViewModelInitException : Exception
{
    public MainPageViewModelInitException(string message, Exception innerException) : base(message, innerException) {}
}
