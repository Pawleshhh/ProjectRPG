namespace ProjectRPG.Core;

internal class SingleRollBase : ISingleRoll
{
    public IRandomNumberGenerator Rng { get; init; } = IRandomNumberGenerator.Default;

    public Dice Dice { get; private set; } = Dice.D6;

    public int DiceCount { get; private set; } = 1;

    public int Factor { get; private set; } = 0;

    public Operator FactorOperator { get; private set; } = Operator.Add;

    public FloatRoundOperation FloatRoundOperation { get; private set; } = FloatRoundOperation.Round;

    public ISingleRoll SetDice(Dice dice)
    {
        Dice = dice;
        return this;
    }

    public ISingleRoll SetDice(Dice dice, int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Number of dices cannot be less than 0");
        }

        DiceCount = count;
        return SetDice(dice);
    }

    public ISingleRoll SetFactor(int factor, Operator @operator)
    {
        Factor = factor;
        FactorOperator = @operator;
        return this;
    }

    public ISingleRoll SetFactor(int factor, Operator @operator, FloatRoundOperation floatRoundOperation)
    {
        FloatRoundOperation = floatRoundOperation;
        return SetFactor(factor, @operator);
    }

    public int Roll()
    {
        var rolledDiceValue =
            Enumerable.Range(0, DiceCount)
                .Select(d => Rng.NextInt(1, Dice.SideCount + 1))
                .Sum();

        if (FactorOperator == Operator.Divide)
        {
            return FloatRoundOperation.ApplyOnDouble((double)rolledDiceValue / Factor);
        }

        return FactorOperator
            .ApplyOp(
                rolledDiceValue,
                Factor);
    }

    public override string ToString()
    {
        string factor = Factor < 0 ? $"({Factor})" : Factor.ToString();
        string dice = DiceCount > 1 ? $"{DiceCount}{Dice}" : Dice.ToString();
        return $"{dice} {FactorOperator.GetOpCharacter()} {factor}";
    }

    public bool Equals(IRoll? other)
    {
        if (other is not ISingleRoll singleRoll)
        {
            return false;
        }

        return Equals(singleRoll);
    }

    public bool Equals(ISingleRoll? other)
    {
        return this.Equals(other, () =>
            Dice.Equals(other!.Dice)
            && DiceCount == other.DiceCount
            && Factor == other.Factor
            && FactorOperator == other.FactorOperator
            && FloatRoundOperation == other.FloatRoundOperation);
    }

    public override bool Equals(object? obj)
    {
        return (this as ISingleRoll).ObjectEquals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Dice, DiceCount, Factor, FactorOperator, FloatRoundOperation);
    }
}
