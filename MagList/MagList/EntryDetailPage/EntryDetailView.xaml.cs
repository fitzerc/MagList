namespace MagList.EntryDetailPage;

public partial class EntryDetailView : ContentPage
{
	public EntryDetailView(EntryDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}