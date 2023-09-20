namespace ProjectRPG.Core;

internal class BaseRoll : IRoll
{
    public IRandomNumberGenerator Rng { get; private set; } = IRandomNumberGenerator.Default;

    public Dice Dice { get; private set; } = Dice.D6;

    public int DiceCount { get; private set; } = 1;

    public int Factor { get; private set; } = 0;

    public Operator FactorOperator { get; private set; } = Operator.Add;

    public FloatRoundOperation FloatRoundOperation { get; private set; } = FloatRoundOperation.Round;

    public IRoll SetRng(IRandomNumberGenerator rng)
    {
        Rng = rng;
        return this;
    }

    public IRoll SetDice(Dice dice)
    {
        Dice = dice;
        return this;
    }

    public IRoll SetDice(Dice dice, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Number of dices cannot be less than 0");
        }

        DiceCount = count;
        return SetDice(dice);
    }

    public IRoll SetFactor(int factor, Operator @operator)
    {
        Factor = factor;
        FactorOperator = @operator;
        return this;
    }

    public IRoll SetFactor(int factor, Operator @operator, FloatRoundOperation floatRoundOperation)
    {
        FloatRoundOperation = floatRoundOperation;
        return SetFactor(factor, @operator);
    }

    public int Roll()
    {
        return FactorOperator
            .ApplyOp(
                Enumerable.Range(1, DiceCount + 1).Select(
                    d => Rng.NextInt(1, Dice.SideCount + 1)).Sum(),
                Factor);
    }

    public override string ToString()
    {
        return $"{Dice} {FactorOperator.GetOpCharacter()} {Factor}";
    }

}
