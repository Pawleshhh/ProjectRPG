namespace ProjectRPG.Core;

public interface IMultipleRoll : IRoll, IEquatable<IMultipleRoll>
{

    public IEnumerable<IRoll> Rolls { get;}

    public IMultipleRoll AddRolls(params IRoll[] rolls);
    public IMultipleRoll ClearRolls();

    public static IMultipleRoll Create(IRandomNumberGenerator rng) 
        => new MultipleRollBase()
        {
            Rng = rng,
        };

}
