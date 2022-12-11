using MagList.Data.Models;
using MagList.Data.Read;
using MagList.MainPage;
using MagList.Test.MainPage.EntriesListView;
using MagList.Test.Mocks;

namespace MagList.Test.MainPage;

public class MainPageViewModelTests
{
    private readonly IEntryReader _mockEntryReader = new MockEntryReader();
    private readonly IEntryReader _badEntryReader = new MockBadEntryReader();

    private readonly IListReader _mockListReader = new MockListReader();
    private readonly IListReader _mockBadListReader = new MockBadListReader();

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
    public void Constructor_BadEntryReader_Test()
    {
        Action constructorAction = () => new MainPageViewModel(null, _mockListReader, null);
        Assert.Throws<ArgumentNullException>(constructorAction);
    }

    [Fact]
    public void Constructor_BadListReader_Test()
    {
        Action constructorAction = () => new MainPageViewModel(_mockEntryReader, null, null);
        Assert.Throws<ArgumentNullException>(constructorAction);
    }

    private MainPageViewModel GetMainPageViewModel(EventHandler<ListModel>? listChanged = null)
        => new MainPageViewModel(_mockEntryReader, _mockListReader, new EntriesListViewModelTests().GetEntriesListViewModel());
}