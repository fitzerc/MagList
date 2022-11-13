using MagList.Data.Read;
using MagList.Data.Write;
using MagList.EntryDetailPage;
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
		builder.Services.AddTransient<IEntryWriter, SqliteEntryWriter>();
		builder.Services.AddTransient<IEntryReader, SqliteEntryReader>();
        builder.Services.AddTransient<IListWriter, SqliteListWriter>();
        builder.Services.AddTransient<IListReader, SqliteListReader>();
        builder.Services.AddTransient<ITagReader, SqliteTagReader>();
        builder.Services.AddTransient<ITagWriter, SqliteTagWriter>();
		builder.Services.AddSingleton<MainPage.MainPage>();
		builder.Services.AddSingleton<MainPage.MainPageViewModel>();
		builder.Services.AddSingleton<ListPage.ListPage>();
        builder.Services.AddTransient<EntryDetailViewModel>((sp) =>
        {
			//TODO: move somewhere that makes more sense?
			var entryDetailVm = new EntryDetailViewModel(sp.GetService<IEntryWriter>(), sp.GetService<ITagReader>());

            var tagWriter = sp.GetService<ITagWriter>();

            entryDetailVm.TagAdded += (sender, model) => tagWriter.Write(model);
            entryDetailVm.TagRemoved += (ConversationSenders, model) => tagWriter.Delete(model.Id);

            return entryDetailVm;
        });

        builder.Services.AddTransient<EntryDetailView>();

		return builder.Build();
	}
}
