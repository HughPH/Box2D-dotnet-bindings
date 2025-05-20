using Box2D;
using System.Linq;

namespace UnitTests;

public class ConcurrentHashSetTests
{
    [Fact]
    public void Add_Remove_Contains_Work()
    {
        var set = new ConcurrentHashSet<int>();
        Assert.True(set.Add(1));
        Assert.False(set.Add(1));
        Assert.True(set.Contains(1));
        Assert.Equal(1, set.Count);
        Assert.True(set.Remove(1));
        Assert.False(set.Contains(1));
        Assert.Equal(0, set.Count);
    }

    [Fact]
    public void Enumeration_ReturnsAllItems()
    {
        var set = new ConcurrentHashSet<int>();
        foreach (int i in Enumerable.Range(0,5))
            set.Add(i);
        var items = set.Items.ToList();
        Assert.Equal(5, items.Count);
        for (int i = 0; i < 5; i++)
            Assert.Contains(i, items);
    }
}
