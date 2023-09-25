namespace ProjectRPG.Core;

public interface ISingleRoll : IRoll, IEquatable<ISingleRoll>
{

    public Dice Dice { get; }
    public int DiceCount { get; }
    public int Factor { get; }
    public Operator FactorOperator { get; }
    public FloatRoundOperation FloatRoundOperation { get; }

    public ISingleRoll SetDice(Dice dice);
    public ISingleRoll SetDice(Dice dice, int count);
    public ISingleRoll SetFactor(int factor, Operator @operator);
    public ISingleRoll SetFactor(int factor, Operator @operator, FloatRoundOperation floatRoundOperation);

    public static ISingleRoll Create(IRandomNumberGenerator rng)
        => new SingleRollBase()
        {
            Rng = rng,
        };

}
