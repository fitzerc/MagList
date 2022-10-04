using MagList.Data.Read;
using MagList.Data.Write;
using Microsoft.Extensions.DependencyInjection;

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

		builder.Services.AddTransient<IEntryWriter, MockEntryWriter>();
		builder.Services.AddTransient<IEntryReader, MockEntryReader>();
		builder.Services.AddSingleton<MainPage.MainPage>();
		builder.Services.AddSingleton<MainPage.MainPageViewModel>();
		builder.Services.AddSingleton<ListPage.ListPage>();

		return builder.Build();
	}
}
