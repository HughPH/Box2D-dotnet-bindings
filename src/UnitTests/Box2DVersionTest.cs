namespace UnitTests;

public class Box2DVersionTest
{
    [Fact]
    public void CheckVersion()
    {
        string? error = null;
        Box2D.Core.SetAssertFunction((condition, name, number) =>
        {
            error = condition;
            return 0;
        });
        
        var version = Box2D.Core.GetVersion();
        string versionString = $"v{version.Major}.{version.Minor}.{version.Revision}";
        Assert.Equal("v3.1.0", versionString);
        
        if (error is not null) Assert.Fail(error);
    }
    
}