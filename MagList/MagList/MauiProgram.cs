using MagList.Data.Models;
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

        var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        path = Path.Combine(path, "maglist.db3");

        builder.Services.AddSingleton(new SQLiteConnection(path));
        builder.Services.AddDataReaders();
        builder.Services.AddDataWriters();


		builder.Services.AddPages();
        builder.Services.AddViewModels();

        var sp = builder.Services.BuildServiceProvider();
		SetupInitialAppData(
            sp.GetService<IListReader>(),
            listModel => sp.GetService<IListWriter>().Write(listModel)
            );

		return builder.Build();
	}

	//Temporary
    public static void SetupInitialAppData(IListReader listReader, Action<ListModel> writeList)
    {
        if (!listReader.GetAll().Any())
        {
			writeList.Invoke(new ListModel{Name = "Default"});
        }
    }
}
