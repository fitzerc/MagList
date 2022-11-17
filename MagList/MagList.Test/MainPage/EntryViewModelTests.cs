using MagList.Data.Models;
using MagList.MainPage;

namespace MagList.Test.MainPage;

public class EntryViewModelTests
{
    [Fact]
    public void ToEntryModel_Test()
    {
        var vm = GetEntryViewModel();
        var expectedModel = GetEntryModel();
        var resultModel = vm.ToEntryModel();

        Assert.True(expectedModel.Equals(resultModel));
    }

    [Fact]
    public void FromEntryModel_Test()
    {
        var model = GetEntryModel();
        var expectedVm = GetEntryViewModel();
        var resultVm = model.ToEntryViewModel();

        Assert.True(expectedVm.Equals(resultVm));
    }

    [Fact]
    public void Equals_Test()
    {
        var sut = GetEntryViewModel();
        var expectedToEqualVm = GetEntryViewModel();

        Assert.Equal(expectedToEqualVm.Id, sut.Id);
        Assert.Equal(expectedToEqualVm.Name, sut.Name);
        Assert.Equal(expectedToEqualVm.Description, sut.Description);
        Assert.Equal(expectedToEqualVm.Order, sut.Order);
    }

    [Fact]
    public void Equals_NullArgument_Test()
    {
        var vm = GetEntryViewModel();
        Assert.Throws(typeof(ArgumentNullException), () => vm.Equals(null));
    }

    private EntryViewModel GetEntryViewModel() => new()
    {
        Id = 1,
        Name = "Test",
        Description = "Test Description",
        Order = 0
    };

    private EntryModel GetEntryModel() => new()
    {
        Id = 1,
        Name = "Test",
        Description = "Test Description",
        Order = 0
    };
}