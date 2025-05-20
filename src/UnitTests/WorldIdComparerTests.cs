using Box2D;
using Box2D.Comparers;

namespace UnitTests;

public class WorldIdComparerTests
{
    [Fact]
    public void WorldComparer_DistinguishesDifferentWorlds()
    {
        var worldA = World.CreateWorld(new WorldDef());
        var worldB = World.CreateWorld(new WorldDef());
        var comparer = WorldComparer.Instance;

        Assert.True(comparer.Equals(worldA, worldA));
        Assert.False(comparer.Equals(worldA, worldB));
        Assert.Equal(0, comparer.Compare(worldA, worldA));
        Assert.NotEqual(0, comparer.Compare(worldA, worldB));

        var idComparer = WorldIdComparer.Instance;
        Assert.True(idComparer.Equals(worldA.id, worldA.id));
        Assert.False(idComparer.Equals(worldA.id, worldB.id));

        worldA.Destroy();
        worldB.Destroy();
    }
}
