using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using MagList.MainPage;

namespace MagList.EntryDetailPage;

public partial class EntryDetailViewModel : ObservableObject, IQueryAttributable
{
    public const string LIST_NAME_PARAM_NAME = "listName";
    private readonly IEntryWriter _entryWriter;
    private readonly ITagReader _tagReader;

    public EventHandler<TagModel> TagAdded;
    public EventHandler<TagModel> TagRemoved;

    public EntryDetailViewModel(IEntryWriter entryWriter, ITagReader tagReader)
    {
        _entryWriter = entryWriter;
        _tagReader = tagReader;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query[nameof(EntryViewModel)] is not EntryViewModel entryVmParam)
        {
            throw new NullReferenceException("EntryViewModel must be passed");
        }

        entryVm = entryVmParam;

        listName = query[LIST_NAME_PARAM_NAME] as string;
        OnPropertyChanged(nameof(EntryVm));

        foreach (var tag in _tagReader.GetTagsForEntry(entryVm.Id))
        {
            tags.Add(tag);
        }
    }

    [ObservableProperty]
    private string newTag;

    [ObservableProperty]
    EntryViewModel entryVm;

    [ObservableProperty]
    ObservableCollection<TagModel> tags = new ObservableCollection<TagModel>();

    [ObservableProperty]
    string listName = "List Name Here";

    [RelayCommand]
    void SaveClicked()
    {
        _entryWriter.Update(EntryViewModel.ToEntryModel(entryVm));
    }

    [RelayCommand]
    void TagAddClicked()
    {
        var newTagModel = new TagModel
        {
            Name = newTag,
            EntryId = entryVm.Id
        };

        tags.Add(newTagModel);
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