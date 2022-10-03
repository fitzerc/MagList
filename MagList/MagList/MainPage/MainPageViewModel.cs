using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MagList.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> entryList = new ObservableCollection<string>();

        [ObservableProperty]
        int count;

        [ObservableProperty]
        string newEntryName = "";

        [RelayCommand]
        void ButtonClicked()
        {
            EntryList.Add(NewEntryName);
            NewEntryName = "";
        }
    }
}
