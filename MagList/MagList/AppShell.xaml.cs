using MagList.EntryDetailPage;

namespace MagList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(EntryDetailView), typeof(EntryDetailView));
	}
}
