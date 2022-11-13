using MagList.Data.Read;
using MagList.Data.Write;
using MagList.EntryDetailPage;
using MagList.MainPage;
using MagList.Test.Mocks;

namespace MagList.Test.EntryDetailPage;

public class EntryDetailViewModelTests
{
    public const string LIST_NAME = "Test List";
    public const string ENTRY_VIEW_MODEL_NAME = "EntryViewModel";
    public IEntryWriter mockEntryWriter;
    public ITagReader mockTagReader;
    public ITagWriter mockTagWriter;

    public EntryViewModel EntryVm;

    [Fact]
    public void ApplyQueryAttributes_Test()
    {
        var sut = GetSut();
        var navQuery = GetNavQuery();

        sut.ApplyQueryAttributes(navQuery);

        Assert.Equal(EntryVm.Name, sut.EntryVm.Name);
        Assert.Equal(LIST_NAME, sut.ListName);
        Assert.Equal(1, 1);
    }

    [Fact]
    public void ApplyQueryAttributes_MissingEntryViewModel_Test()
    {
        var sut = GetSut();

        var dict = new Dictionary<string, object>();
        dict.Add("EntryViewModel", false);

        var applyQueryAttAction = () => sut.ApplyQueryAttributes(dict);

        Assert.Throws<NullReferenceException>(applyQueryAttAction);
    }

    [Fact]
    public void TagAddClicked_Test()
    {
        var sut = GetSut();
        sut.ApplyQueryAttributes(GetNavQuery());

        Assert.Null(sut.Tags.FirstOrDefault(x => x.Name == "New Tag"));

        sut.NewTag = "New Tag";
        sut.TagAddClickedCommand.Execute(null);

        Assert.NotNull(sut.Tags.FirstOrDefault(x => x.Name == "New Tag"));
    }

    [Fact]
    public void SaveClicked_Test()
    {
        var sut = GetSut();
        sut.ApplyQueryAttributes(GetNavQuery());
        sut.EntryVm.Description = "New Description";

        sut.SaveClickedCommand.Execute(null);

        Assert.Equal(
            "New Description", 
            (mockEntryWriter as MockEntryWriter).LastUpdateModel.Description);
    }

    [Fact]
    public void RemoveTagClicked_Test()
    {
        var sut = GetSut();
        sut.ApplyQueryAttributes(GetNavQuery());
        var tagToRemove = sut.Tags.First();

        Assert.NotNull(sut.Tags.FirstOrDefault(x => x.Id == tagToRemove.Id));
        sut.RemoveTagClickedCommand.Execute(tagToRemove);
        Assert.Null(sut.Tags.FirstOrDefault(x => x.Id == tagToRemove.Id));
    }

    public EntryDetailViewModel GetSut()
    {
        mockEntryWriter = new MockEntryWriter();
        mockTagReader = new MockTagReader();
        mockTagWriter = new MockTagWriter();

        return new EntryDetailViewModel(mockEntryWriter, mockTagReader, mockTagWriter);
    }

    public IDictionary<string, object> GetNavQuery()
    {
        EntryVm = new EntryViewModel
        {
            Id = 1,
            ListId = 1,
            Name = "test",
            Description = "test description",
            Order = 1
        };

        return new Dictionary<string, object>
        {
            {ENTRY_VIEW_MODEL_NAME, EntryVm},
            {EntryDetailViewModel.LIST_NAME_PARAM_NAME, LIST_NAME}
        };
    }
}