namespace ProjectRPG.Core.Test;

[TestFixture]
internal class DefaultRngTest
{

    [Test]
    public void Constructor_DefaultSeed_SetToDefaultValue()
    {
        // Arrange & Act
        var rng = new DefaultRng();

        // Assert
        Assert.That(rng.Seed, Is.EqualTo(0));
    }

    [Test]
    public void Constructor_SettingSeed_SetToGivenValue()
    {
        // Arrange
        int seed = 11;

        // Act
        var rng = new DefaultRng(seed);

        // Assert
        Assert.That(rng.Seed, Is.EqualTo(seed));
    }

    [Test]
    public void Reset_WithoutNewSeed_SeedDidNotChange()
    {
        // Arrange
        int seed = 5;
        var rng = new DefaultRng(seed);

        // Act
        rng.Reset();

        // Assert
        Assert.That(rng.Seed, Is.EqualTo(seed));
    }

    [Test]
    public void Reset_WithoutNewSeed_NoExceptionThrown()
    {
        // Arrange
        int seed = 5;
        var rng = new DefaultRng(seed);

        // Act
        rng.Reset();

        // Assert
        _ = rng.NextInt();
        _ = rng.NextInt(5);
        _ = rng.NextInt(5, 10);
        _ = rng.NextDouble();
    }

    [Test]
    public void Reset_WithNewSeed_NewSeedSet()
    {
        // Arrange
        int seed = 5;
        int newSeed = 10;
        var rng = new DefaultRng(seed);

        // Act
        rng.Reset(newSeed);

        // Assert
        Assert.That(rng.Seed, Is.EqualTo(newSeed));
    }

    [Test]
    public void Reset_WithNewSeed_NoExceptionThrown()
    {
        // Arrange
        int seed = 5;
        int newSeed = 10;
        var rng = new DefaultRng(seed);

        // Act
        rng.Reset(newSeed);

        // Assert
        _ = rng.NextInt();
        _ = rng.NextInt(5);
        _ = rng.NextInt(5, 10);
        _ = rng.NextDouble();
    }
}
