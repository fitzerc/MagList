using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MagList.Data.Models;
using System.Collections.ObjectModel;

namespace MagList.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<EntryModel> entryList = new ObservableCollection<EntryModel>();

        [ObservableProperty]
        string newEntryName = "";

        [RelayCommand]
        void AddClicked()
        {
            EntryList.Add(new EntryModel() { Name = NewEntryName, Description = $"Description for {NewEntryName}", Order = -1 });
            NewEntryName = "";
        }

        [RelayCommand]
        void DeleteClicked(EntryModel entry)
        {
            EntryList.Remove(entry);
        }
    }
}
