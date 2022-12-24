using System.Collections.ObjectModel;
using MagList.Data.Read;
using MagList.MainPage;
using MagList.MainPage.EntriesListView;
using MagList.Test.Mocks;

namespace MagList.Test.MainPage.EntriesListView;

public class EntriesListViewModelTests
{
    private readonly IEntryReader _mockEntryReader = new MockEntryReader();
    private readonly IEntryReader _badEntryReader = new MockBadEntryReader();

    private readonly IListReader _mockListReader = new MockListReader();
    private readonly IListReader _mockBadListReader = new MockBadListReader();

    private EntryViewModel LastEntryTapped = new EntryViewModel();
    private List<EntryViewModel> SortOrderChangedParam;
    private EntryViewModel EntryDeletedParam;

    [Fact]
    public void DeleteCommand_Test()
    {
        var expectedToBeDeleted = (_mockEntryReader as MockEntryReader)._entries[0].ToEntryViewModel();
        var sut = GetEntriesListViewModel();

        sut.DeleteClickedCommand.Execute(expectedToBeDeleted);

        Assert.Equal(expectedToBeDeleted.Name, EntryDeletedParam.Name);
        Assert.DoesNotContain(expectedToBeDeleted, sut.EntryList);
    }

    [Fact]
    //TODO: double check, I'm not sure this is even right
    public void DragFirstItem_DownOneRow_Test()
    {
        var sut = GetEntriesListViewModel();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[2]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[2]);

        Assert.Equal(1, sut.EntryList[2].Id);
        Assert.Equal(1, SortOrderChangedParam[2].Id);
    }

    [Fact]
    //TODO: double check, I'm not sure this is even right
    public void DragFirstItem_DownTwoRows_Test()
    {
        var sut = GetEntriesListViewModel();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[2]);
        sut.ItemDragLeaveCommand.Execute(sut.EntryList[2]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[3]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[3]);

        Assert.Equal(1, sut.EntryList[3].Id);
        Assert.Equal(1, SortOrderChangedParam[3].Id);
    }

    [Fact]
    public void DragFirstItem_DropOnSelf_Test()
    {
        var sut = GetEntriesListViewModel();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[0]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[0]);

        Assert.Equal(1, sut.EntryList[0].Id);
    }

    [Fact]
    public void EntryTapped_Test()
    {
        var sut = GetEntriesListViewModel();
        sut.EntryTappedCommand.Execute(sut.EntryList[0]);
        Assert.Equal(LastEntryTapped.Name, sut.EntryList[0].Name);
    }

    private ObservableCollection<EntryViewModel> GetViewModels()
    {
        var models = _mockEntryReader.GetAllInList(1);
        var viewModels = new ObservableCollection<EntryViewModel>();

        foreach (var model in models)
        {
            viewModels.Add(model.ToEntryViewModel());
        }

        return viewModels;
    }

    public EntriesListViewModel GetEntriesListViewModel()
    {
        var sut = new EntriesListViewModel(
            GetEntryTappedFunc(),
            GetSortOrderChanged(),
            GetDeleteEvent());

        sut.CurrentList = _mockListReader.Get(1);
        sut.EntryList = GetViewModels();

        return sut;
    }

    private Func<string, Dictionary<string, object>, Task> GetEntryTappedFunc()
    {
        return (s, objects) =>
            Task.Factory.StartNew(() =>
                LastEntryTapped = objects.First(x => x.Key == nameof(EntryViewModel)).Value as EntryViewModel ?? throw new InvalidOperationException());
    }

    private EventHandler<IEnumerable<EntryViewModel>> GetSortOrderChanged()
    {
        SortOrderChangedParam = new List<EntryViewModel>();
        return (sender, models)
            => SortOrderChangedParam = models.ToList();
    }

    private EventHandler<EntryViewModel> GetDeleteEvent()
    {
        EntryDeletedParam = new EntryViewModel();
        return (sender, model) => EntryDeletedParam = model;
    }
}