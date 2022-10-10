using CommunityToolkit.Mvvm.ComponentModel;
using MagList.Data.Models;

namespace MagList.MainPage;

public partial class EntryViewModel : ObservableObject
{
    [ObservableProperty]
    int id;

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

    //TODO: doesn't need to be static
    public static EntryModel ToEntryModel(EntryViewModel entryVm)
    {
        if (entryVm == null)
        {
            throw new ArgumentNullException(nameof(entryVm));
        }

        return new EntryModel
        {
            Id = entryVm.Id,
            Name = entryVm.Name,
            Description = entryVm.Description,
            Order = entryVm.Order
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
