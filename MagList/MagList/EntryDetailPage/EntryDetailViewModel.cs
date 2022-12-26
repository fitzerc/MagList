using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using MagList.Data.Read;
using MagList.MainPage;
using MagList.State;

namespace MagList.EntryDetailPage;

public partial class EntryDetailViewModel : ObservableObject, IQueryAttributable
{
    public const string LIST_NAME_PARAM_NAME = "listName";
    private readonly AppState _appState;

    public EventHandler<TagModel> TagAdded;
    public EventHandler<TagModel> TagRemoved;
    public EventHandler<EntryViewModel> EntrySaved;

    public EntryDetailViewModel(AppState appState)
    {
        _appState = appState;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //TODO: should use appState instead of nav param?
        if (query[nameof(EntryViewModel)] is not EntryViewModel entryVmParam)
        {
            throw new NullReferenceException("EntryViewModel must be passed");
        }

        entryVm = entryVmParam;

        listName = query[LIST_NAME_PARAM_NAME] as string;
        OnPropertyChanged(nameof(EntryVm));

        Tags = _appState.CurrentEntryDetailState.Tags;
    }

    [ObservableProperty]
    private string newTag;

    [ObservableProperty]
    EntryViewModel entryVm;

    [ObservableProperty]
    ObservableCollection<TagModel> tags;

    [ObservableProperty]
    string listName = "List Name Here";

    [RelayCommand]
    void SaveClicked()
    {
        EntrySaved.Invoke(this, entryVm);
    }

    [RelayCommand]
    void TagAddClicked()
    {
        var newTagModel = new TagModel
        {
            Name = newTag,
            EntryId = entryVm.Id
        };

        TagAdded.Invoke(this, newTagModel);

        NewTag = "";
    }

    [RelayCommand]
    void RemoveTagClicked(TagModel tagToDel)
    {
        tags.Remove(tagToDel);
        TagRemoved.Invoke(this, tagToDel);
    }
}