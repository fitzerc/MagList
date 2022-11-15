using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using MagList.EntryDetailPage;
using MagList.MainPage;
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
		builder.Services.AddSingleton<MainPage.MainPageViewModel>(sp =>
        {
            var mainPageVm = new MainPageViewModel(
                sp.GetService<IEntryReader>(),
                sp.GetService<IListReader>(),
                (sender, model) => sp.GetService<IListWriter>().Write(model));

            var entryWriter = sp.GetService<IEntryWriter>();

            mainPageVm.EntryAdded += (sender, model) =>
                entryWriter.Write(EntryViewModel.ToEntryModel(model));

            mainPageVm.EntryDeleted += (sender, model) =>
                entryWriter.Delete(model.Id);

            mainPageVm.SortOrderChanged += (sender, models) =>
            {
                //TODO: unit test an empty list
                var modelList = models.Select(entry => EntryViewModel.ToEntryModel(entry)).ToList();
                entryWriter.UpdateAll(modelList);
            };

            return mainPageVm;
        });
		builder.Services.AddSingleton<ListPage.ListPage>();
        builder.Services.AddTransient<EntryDetailViewModel>((sp) =>
        {
			//TODO: move somewhere that makes more sense?
			var entryDetailVm = new EntryDetailViewModel(sp.GetService<ITagReader>());

            var tagWriter = sp.GetService<ITagWriter>();

            entryDetailVm.TagAdded += (sender, model) => tagWriter.Write(model);
            entryDetailVm.TagRemoved += (sender, model) => tagWriter.Delete(model.Id);
            entryDetailVm.EntrySaved += (sender, model) => sp.GetService<IEntryWriter>().Update(EntryViewModel.ToEntryModel(model));

            return entryDetailVm;
        });

        builder.Services.AddTransient<EntryDetailView>();

		return builder.Build();
	}
}
