using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using MagList.Data.Write;
using MagList.MainPage;

namespace MagList.EntryDetailPage;

public partial class EntryDetailViewModel : ObservableObject, IQueryAttributable
{
    public const string LIST_NAME_PARAM_NAME = "listName";
    private readonly IEntryWriter _entryWriter;

    public EntryDetailViewModel(IEntryWriter entryWriter)
    {
        _entryWriter = entryWriter;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        entryVm = query[nameof(EntryViewModel)] as EntryViewModel;
        listName = query[LIST_NAME_PARAM_NAME] as string;
        OnPropertyChanged(nameof(EntryVm));
    }

    [ObservableProperty]
    EntryViewModel entryVm;

    [ObservableProperty]
    private string listName = "List Name Here";

    [RelayCommand]
    void SaveClicked()
    {
        _entryWriter.Update(EntryViewModel.ToEntryModel(entryVm));
    }
}