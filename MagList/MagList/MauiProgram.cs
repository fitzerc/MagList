using MagList.Data.Read;
using MagList.Data.Write;
using SQLite;

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

        var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        path = Path.Combine(path, "maglist.db3");

        builder.Services.AddSingleton(new SQLiteConnection(path));
		//builder.Services.AddTransient<IEntryWriter, MockEntryWriter>();
		builder.Services.AddTransient<IEntryWriter, SqliteEntryWriter>();
		//builder.Services.AddTransient<IEntryReader, MockEntryReader>();
		builder.Services.AddTransient<IEntryReader, SqliteEntryReader>();
		builder.Services.AddSingleton<MainPage.MainPage>();
		builder.Services.AddSingleton<MainPage.MainPageViewModel>();
		builder.Services.AddSingleton<ListPage.ListPage>();

		return builder.Build();
	}
}
