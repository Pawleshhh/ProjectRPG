namespace ProjectRPG.Core;

public interface IRoll
{

    public IRandomNumberGenerator Rng { get; }

    public Dice Dice { get; }
    public int DiceCount { get; }
    public int Factor { get; }
    public Operator FactorOperator { get; }
    public FloatRoundOperation FloatRoundOperation { get; }

    public IRoll SetRng(IRandomNumberGenerator rng);

    public IRoll SetDice(Dice dice);
    public IRoll SetDice(Dice dice, int count);
    public IRoll SetFactor(int factor, Operator @operator);
    public IRoll SetFactor(int factor, Operator @operator, FloatRoundOperation floatRoundOperation);

    public int Roll();

    public static IRoll Create() => new();

}
