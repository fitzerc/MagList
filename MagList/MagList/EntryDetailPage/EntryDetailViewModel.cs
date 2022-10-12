using CommunityToolkit.Mvvm.ComponentModel;
using MagList.MainPage;

namespace MagList.EntryDetailPage;

public partial class EntryDetailViewModel : ObservableObject, IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        entryVm = query[nameof(EntryViewModel)] as EntryViewModel;
    }

    [ObservableProperty]
    EntryViewModel entryVm;
}