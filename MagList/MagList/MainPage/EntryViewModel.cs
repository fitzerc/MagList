using CommunityToolkit.Mvvm.ComponentModel;
using MagList.Data.Models;

namespace MagList.MainPage;

public partial class EntryViewModel : ObservableObject
{
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

    public static EntryModel ToEntryModel(EntryViewModel vm)
    {
        return new EntryModel
        {
            Name = vm.Name,
            Description = vm.Description,
            Order = vm.Order
        };
    }

    public static EntryViewModel FromEntryModel(EntryModel entry)
    {
        return new EntryViewModel
        {
            Name = entry.Name,
            Description = entry.Description,
            Order = entry.Order
        };
    }
}
