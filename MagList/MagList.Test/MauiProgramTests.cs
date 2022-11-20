using MagList.Data.Models;
using MagList.Test.Mocks;

namespace MagList.Test;

public class MauiProgramTests
{
    public const string DEFAULT_LIST_NAME = "Default";

    [Fact]
    public void SetupInitialAppData_Test()
    {
        var mockDataReader = new MockListReader();
        mockDataReader.CurrentList = null;

        MauiProgram.SetupInitialAppData(
            mockDataReader,
            (model => mockDataReader.CurrentList = model)
            );

        Assert.Equal(DEFAULT_LIST_NAME, mockDataReader.CurrentList.Name);
    }
}