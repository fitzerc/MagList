using MagList.Data.Read;
using MagList.Data.Write;
using MagList.EntryDetailPage;
using MagList.MainPage;
using MagList.MainPage.EntriesListView;
using MagList.State;

namespace MagList
{
    public static class DependencyInjection
    {
        public static void AddDataReaders(this IServiceCollection services)
        {
            services.AddTransient<IEntryReader, SqliteEntryReader>();
            services.AddTransient<IListReader, SqliteListReader>();
            services.AddTransient<ITagReader, SqliteTagReader>();
        }

        public static void AddDataWriters(this IServiceCollection services)
        {
            services.AddTransient<IEntryWriter, SqliteEntryWriter>();
            services.AddTransient<IListWriter, SqliteListWriter>();
            services.AddTransient<ITagWriter, SqliteTagWriter>();
        }

        public static void AddPages(this IServiceCollection services)
        {
            services.AddSingleton<MainPage.MainPage>();
            services.AddSingleton<ListPage.ListPage>();
        }

        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<EntriesListViewModel>(sp =>
            {
                var appStateActions = sp.GetService<AppStateActions>();

                return new EntriesListViewModel(
                    async (viewName, navParams) =>
                    {
                        if (navParams[nameof(EntryViewModel)] is not EntryViewModel entryVmParam)
                        {
                            throw new NullReferenceException("EntryViewModel must be passed");
                        }

                        appStateActions.SetCurrentEntryDetailState(entryVmParam.ToEntryModel());
                        await Shell.Current.GoToAsync(viewName, navParams);
                    },
                    (sender, models) =>
                    {
                        var modelList = models.Select(entry => entry.ToEntryModel());
                        appStateActions.UpdateAllEntries(modelList);
                    },
                    (sender, viewModel) => appStateActions.DeleteEntry(viewModel.ToEntryModel()),
                    sp.GetService<AppState>());
            });


            services.AddSingleton<MainPage.MainPageViewModel>(sp =>
            {
                var mainPageVm = new MainPageViewModel(
                    sp.GetService<EntriesListViewModel>(),
                    sp.GetService<AppState>());

                mainPageVm.EntryAdded += (sender, viewModel) =>
                    sp.GetService<AppStateActions>().AddEntry(viewModel.ToEntryModel());

                return mainPageVm;
            });

            services.AddTransient<EntryDetailViewModel>((sp) =>
            {
                var entryDetailVm = new EntryDetailViewModel(sp.GetService<AppState>());

                var appStateActions = sp.GetService<AppStateActions>();

                entryDetailVm.TagAdded += (sender, model) => appStateActions.AddTag(model);
                entryDetailVm.TagRemoved += (sender, model) => appStateActions.DeleteTag(model);
                entryDetailVm.EntrySaved += (sender, viewModel) => appStateActions.UpdateEntry(viewModel.ToEntryModel());

                return entryDetailVm;
            });

            services.AddTransient<EntryDetailView>();
        }
    }
}
