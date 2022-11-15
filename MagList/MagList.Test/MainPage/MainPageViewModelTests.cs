using MagList.Data.Models;
using MagList.Data.Read;
using MagList.Data.Write;
using MagList.MainPage;
using MagList.Test.Mocks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;

namespace MagList.Test.MainPage;

public class MainPageViewModelTests
{
    private readonly IEntryReader _mockEntryReader = new MockEntryReader();
    private readonly IEntryReader _badEntryReader = new MockBadEntryReader();

    private readonly IListReader _mockListReader = new MockListReader();
    private readonly IListReader _mockBadListReader = new MockBadListReader();

    private ListModel LastListChanged = new ListModel();

    [Fact]
    public void AddCommand_Test()
    {
        EntryViewModel addedEntry = new EntryViewModel{Id = -1};
        var sut = GetMainPageViewModel();
        var expectedAddition = new EntryViewModel()
        {
            ListId = 1,
            Description = $"Description for {"Test 4"}",
            Name = "Test 4",
            Order = -1
        };

        sut.EntryAdded += (sender, model) => addedEntry = model;
        sut.NewEntryName = expectedAddition.Name;
        sut.AddClickedCommand.Execute(sut);

        Assert.Equal(
            expectedAddition.Description,
            sut.EntryList.First(x => x.Name == expectedAddition.Name).Description);

        Assert.Equal(expectedAddition.Id, addedEntry.Id);
    }

    //TODO: change test to not add if all required fields are not filled out
    //  could even set the buttons CanExecute to false in that scenario
    [Fact]
    public void AddCommand_EmptyName_Test()
    {
        var addedEntry = new EntryViewModel();
        var sut = GetMainPageViewModel();

        sut.EntryAdded += (sender, model) => addedEntry = model;
        sut.AddClickedCommand.Execute(sut);

        Assert.Equal("Description for ", addedEntry.Description);
    }

    [Fact]
    public void DeleteCommand_Test()
    {
        var expectedToBeDeleted = EntryViewModel.FromEntryModel((_mockEntryReader as MockEntryReader)._entries[0]);
        var sut = GetMainPageViewModel();
        var entryDeletedParam = new EntryViewModel();
        sut.EntryDeleted += (sender, model) => entryDeletedParam = model;

        sut.DeleteClickedCommand.Execute(expectedToBeDeleted);

        Assert.Equal(expectedToBeDeleted.Name, entryDeletedParam.Name);
        Assert.False(sut.EntryList.Contains(expectedToBeDeleted));
    }

    [Fact]
    //TODO: double check, I'm not sure this is even right
    public void DragFirstItem_DownOneRow_Test()
    {
        var sortOrderChangedParam = new List<EntryViewModel>();
        var sut = GetMainPageViewModel();
        sut.SortOrderChanged += (sender, models)
            => sortOrderChangedParam = models.ToList();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[2]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[2]);

        Assert.Equal(1, sut.EntryList[2].Id);
        Assert.Equal(1, sortOrderChangedParam[2].Id);
    }

    [Fact]
    //TODO: double check, I'm not sure this is even right
    public void DragFirstItem_DownTwoRows_Test()
    {
        var sortOrderChangedParam = new List<EntryViewModel>();
        var sut = GetMainPageViewModel();

        sut.SortOrderChanged += (sender, models)
            => sortOrderChangedParam = models.ToList();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[2]);
        sut.ItemDragLeaveCommand.Execute(sut.EntryList[2]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[3]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[3]);

        Assert.Equal(1, sut.EntryList[3].Id);
        Assert.Equal(1, sortOrderChangedParam[3].Id);
    }

    [Fact]
    public void DragFirstItem_DropOnSelf_Test()
    {
        var sut = GetMainPageViewModel();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[0]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[0]);

        Assert.Equal(1, sut.EntryList[0].Id);
    }

    [Fact]
    public void Constructor_NoDataAccess_Test()
    {
        Assert.Throws<MainPageViewModelInitException>(
            () => new MainPageViewModel(_badEntryReader, _mockBadListReader, GetListChangedHandler()));
    }

    [Fact]
    public void Constructor_NullEntryReader_Test()
    {
        Assert.Throws<ArgumentNullException>(
            () => new MainPageViewModel(null, _mockListReader, GetListChangedHandler()));
    }

    private MainPageViewModel GetMainPageViewModel(EventHandler<ListModel>? listChanged = null)
    {
        listChanged ??= GetListChangedHandler();

        return new MainPageViewModel(_mockEntryReader, _mockListReader, listChanged);
    }

    private EventHandler<ListModel> GetListChangedHandler() => (sender, model) => LastListChanged = model;
}