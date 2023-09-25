namespace ProjectRPG.Core;
/// <summary>
/// Interface for random number generator.
/// </summary>
public interface IRandomNumberGenerator
{

    /// <summary>
    /// Gets seed of the random number generator.
    /// </summary>
    public int Seed { get; }

    public int NextInt();
    public int NextInt(int maxValue);
    public int NextInt(int minValue, int maxValue);

    public double NextDouble();

    public void Reset();
    public void Reset(int seed);

    public static IRandomNumberGenerator Default { get; } = new DefaultRng();

}
