using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagList.Data.Read;
using MagList.Data.Write;
using MagList.EntryDetailPage;
using MagList.MainPage;

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
            services.AddSingleton<MainPage.MainPageViewModel>(sp =>
            {
                var mainPageVm = new MainPageViewModel(
                    sp.GetService<IEntryReader>(),
                    sp.GetService<IListReader>(),
                    (sender, model) => sp.GetService<IListWriter>().Write(model),
                    async (viewName, navParams) => await Shell.Current.GoToAsync(viewName, navParams)
                );

                var entryWriter = sp.GetService<IEntryWriter>();

                mainPageVm.EntryAdded += (sender, viewModel) =>
                    entryWriter.Write(viewModel.ToEntryModel());

                mainPageVm.EntryDeleted += (sender, model) =>
                    entryWriter.Delete(model.Id);

                mainPageVm.SortOrderChanged += (sender, models) =>
                {
                    //TODO: unit test an empty list
                    var modelList = models.Select(entry => entry.ToEntryModel());
                    entryWriter.UpdateAll(modelList);
                };

                return mainPageVm;
            });

            services.AddTransient<EntryDetailViewModel>((sp) =>
            {
                //TODO: move somewhere that makes more sense?
                var entryDetailVm = new EntryDetailViewModel(sp.GetService<ITagReader>());

                var tagWriter = sp.GetService<ITagWriter>();

                entryDetailVm.TagAdded += (sender, model) => tagWriter.Write(model);
                entryDetailVm.TagRemoved += (sender, model) => tagWriter.Delete(model.Id);
                entryDetailVm.EntrySaved += (sender, model) =>
                    sp.GetService<IEntryWriter>().Update(model.ToEntryModel());

                return entryDetailVm;
            });

            services.AddTransient<EntryDetailView>();
        }
    }
}
