using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using System.Collections.ObjectModel;

namespace MagList.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IEntryReader _entryReader;
    private readonly IEntryWriter _entryWriter;

    public MainPageViewModel(IEntryReader entryReader, IEntryWriter entryWriter)
    {
        if (entryReader == null)
        {
            throw new ArgumentNullException(nameof(entryReader));
        }

        if (entryWriter == null)
        {
            throw new ArgumentNullException(nameof(entryWriter));
        }

        _entryReader = entryReader;
        _entryWriter = entryWriter;

        try
        {
            entryList = new ObservableCollection<EntryModel>(_entryReader.GetAll());
        }
        catch (Exception e)
        {
            throw new MainPageViewModelInitException("Unable to initialize data", e);
        }
    }

    [ObservableProperty]
    ObservableCollection<EntryModel> entryList = new ObservableCollection<EntryModel>();

    [ObservableProperty]
    string newEntryName = "";

    [RelayCommand]
    void AddClicked()
    {
        var newEntry = new EntryModel() { Name = NewEntryName, Description = $"Description for {NewEntryName}", Order = -1 };

        _entryWriter?.Write(newEntry);
        EntryList.Add(newEntry);

        NewEntryName = "";
    }

    [RelayCommand]
    void DeleteClicked(EntryModel entry)
    {
        _entryWriter?.Delete(entry);
        EntryList.Remove(entry);
    }
}

public class MainPageViewModelInitException : Exception
{
    public MainPageViewModelInitException(string message, Exception innerException) : base(message, innerException) {}
}
