namespace ProjectRPG.Core;

public interface IRoll : IEquatable<IRoll>
{

    public IRandomNumberGenerator Rng { get; init; }

    public int Roll();

}
