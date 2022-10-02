using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MagList.MainPage
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        int count;

        [RelayCommand]
        void ButtonClicked()
        {
            Count++;
        }
    }
}
