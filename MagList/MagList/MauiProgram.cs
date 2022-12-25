using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using MagList.State;
using SQLite;
using System.Collections.ObjectModel;

namespace MagList;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        path = Path.Combine(path, "maglist.db3");

        builder.Services.AddSingleton(new SQLiteConnection(path));

        var appState = new AppState();

        builder.Services.AddSingleton((sp) => appState);
        builder.Services.AddSingleton<AppStateActions>();

        builder.Services.AddDataReaders();
        builder.Services.AddDataWriters();


		builder.Services.AddPages();
        builder.Services.AddViewModels();

        var sp = builder.Services.BuildServiceProvider();
		var mauiApp = builder.Build();

		SetupInitialAppData(sp, appState, listModel => sp.GetService<IListWriter>().Write(listModel));

        return mauiApp;
	}

	//Temporary?
    public static void SetupInitialAppData(ServiceProvider sp, AppState appState, Action<ListModel> writeList)
    {
        var listReader = sp.GetService<IListReader>();
        var entryReader = sp.GetService<IEntryReader>();

        if (!listReader.GetAll().Any())
        {
			writeList.Invoke(new ListModel{Name = "Default"});
        }

        appState.Lists = new ObservableCollection<ListModel>(listReader.GetAll());
        var firstListId = appState.Lists.First<ListModel>().Id;

        appState.CurrentList = new ListState
        {
            List = listReader.Get(firstListId),
            Entries = new ObservableCollection<EntryModel>(entryReader.GetAllInList(firstListId))
        };
    }
}
