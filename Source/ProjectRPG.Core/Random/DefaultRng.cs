namespace ProjectRPG.Core;

internal class DefaultRng : IRandomNumberGenerator
{

    private Random? random;
    private Random Random => random ??= new Random(Seed);

    public DefaultRng() { }

    public DefaultRng(int seed)
    {
        Seed = seed;
    }

    public int Seed { get; private set; }

    public double NextDouble()
    {
        return Random.NextDouble();
    }

    public int NextInt()
    {
        return Random.Next();
    }

    public int NextInt(int maxValue)
    {
        return Random.Next(maxValue);
    }

    public int NextInt(int minValue, int maxValue)
    {
        return Random.Next(minValue, maxValue);
    }

    public void Reset()
    {
        random = null;
    }

    public void Reset(int seed)
    {
        Seed = seed;
        Reset();
    }
}
