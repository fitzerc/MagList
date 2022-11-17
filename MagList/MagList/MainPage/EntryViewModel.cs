using CommunityToolkit.Mvvm.ComponentModel;
using MagList.Data.Models;

namespace MagList.MainPage;

public partial class EntryViewModel : ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    private int listId;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    int order;

    [ObservableProperty]
    bool isBeingDragged = false;

    [ObservableProperty]
    bool isBeingDraggedOver = false;

    public EntryModel ToEntryModel()
    {
        return new EntryModel
        {
            Id = this.Id,
            ListId = this.ListId,
            Name = this.Name,
            Description = this.Description,
            Order = this.Order
        };
    }

    public static EntryViewModel FromEntryModel(EntryModel entryModel)
    {
        if (entryModel == null)
        {
            throw new ArgumentNullException(nameof(entryModel));
        }

        return new EntryViewModel
        {
            Id = entryModel.Id,
            ListId = entryModel.ListId,
            Name = entryModel.Name,
            Description = entryModel.Description,
            Order = entryModel.Order
        };
    }

    public bool Equals(EntryViewModel entryVm)
    {
        if (entryVm == null)
        {
            throw new ArgumentNullException(nameof(entryVm));
        }

        return entryVm.Id == Id && entryVm.Name == Name && entryVm.Description == Description && entryVm.Order == Order;
    }
}
