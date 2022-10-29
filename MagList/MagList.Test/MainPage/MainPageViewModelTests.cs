using MagList.Data.Read;
using MagList.Data.Write;
using MagList.MainPage;
using MagList.Test.Mocks;

namespace MagList.Test.MainPage;

public class MainPageViewModelTests
{
    private readonly IEntryReader _mockEntryReader = new MockEntryReader();
    private readonly IEntryWriter _mockEntryWriter = new MockEntryWriter();
    private readonly IEntryWriter _badEntryWriter = new MockBadEntryWriter();
    private readonly IEntryReader _badEntryReader = new MockBadEntryReader();

    private readonly IListWriter _mockListWriter = new MockListWriter();
    private readonly IListWriter _mockBadListWriter = new MockBadListWriter();
    private readonly IListReader _mockListReader = new MockListReader();
    private readonly IListReader _mockBadListReader = new MockBadListReader();

    [Fact]
    public void AddCommand_Test()
    {
        var sut = GetMainPageViewModel();
        var expectedAddition = new EntryViewModel()
        {
            ListId = 1,
            Description = $"Description for {"Test 4"}",
            Name = "Test 4",
            Order = -1
        };

        sut.NewEntryName = expectedAddition.Name;

        sut.AddClickedCommand.Execute(sut);

        Assert.Equal(
            expectedAddition.Description,
            sut.EntryList.First(x => x.Name == expectedAddition.Name).Description);

        Assert.Equal(expectedAddition.Id, (_mockEntryWriter as MockEntryWriter).LastWriteId);
    }

    //TODO: change test to not add if all required fields are not filled out
    //  could even set the buttons CanExecute to false in that scenario
    [Fact]
    public void AddCommand_EmptyName_Test()
    {
        var sut = GetMainPageViewModel();

        sut.AddClickedCommand.Execute(sut);

        Assert.Equal(
            "Description for ",
            sut.EntryList.First(x => x.Name == "").Description);
    }

    [Fact]
    public void DeleteCommand_Test()
    {
        var entryToBeDeleted = EntryViewModel.FromEntryModel((_mockEntryReader as MockEntryReader)._entries[0]);
        var sut = GetMainPageViewModel();

        sut.DeleteClickedCommand.Execute(entryToBeDeleted);
        Assert.False(sut.EntryList.Contains(entryToBeDeleted));
    }

    [Fact]
    //TODO: double check, I'm not sure this is even right
    public void DragFirstItem_DownOneRow_Test()
    {
        var sut = GetMainPageViewModel();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[2]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[2]);

        Assert.Equal(1, sut.EntryList[2].Id);
    }

    [Fact]
    //TODO: double check, I'm not sure this is even right
    public void DragFirstItem_DownTwoRows_Test()
    {
        var sut = GetMainPageViewModel();

        sut.ItemDraggedCommand.Execute(sut.EntryList[0]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[2]);
        sut.ItemDragLeaveCommand.Execute(sut.EntryList[2]);
        sut.ItemDraggedOverCommand.Execute(sut.EntryList[3]);
        sut.ItemDroppedCommand.Execute(sut.EntryList[3]);

        Assert.Equal(1, sut.EntryList[3].Id);
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
            () => new MainPageViewModel(_badEntryReader, _badEntryWriter, _mockBadListReader, _mockBadListWriter));
    }

    [Fact]
    public void Constructor_NullEntryWriter_Test()
    {
        Assert.Throws<ArgumentNullException>(
            () => new MainPageViewModel(_badEntryReader, null, _mockBadListReader, _mockBadListWriter));
    }

    [Fact]
    public void Constructor_NullEntryReader_Test()
    {
        Assert.Throws<ArgumentNullException>(
            () => new MainPageViewModel(null, _badEntryWriter, _mockListReader, _mockListWriter));
    }

    private MainPageViewModel GetMainPageViewModel()
    {
        return new MainPageViewModel(_mockEntryReader, _mockEntryWriter, _mockListReader, _mockListWriter);
    }
}