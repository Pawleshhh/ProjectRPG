using System.Text;

namespace ProjectRPG.Core;

internal class MultipleRollBase : IMultipleRoll
{
    public IRandomNumberGenerator Rng { get; init; } = IRandomNumberGenerator.Default;

    private readonly List<IRoll> rolls = new();
    public IEnumerable<IRoll> Rolls => this.rolls;

    public IMultipleRoll AddRolls(params IRoll[] rolls)
    {
        this.rolls.AddRange(rolls);
        return this;
    }

    public IMultipleRoll ClearRolls()
    {
        this.rolls.Clear();
        return this;
    }

    public int Roll()
    {
        return Rolls.Select(x => x.Roll()).Sum();
    }

    private string? toStringResult = null;
    public override string ToString()
    {
        if (this.toStringResult is not null)
        {
            return this.toStringResult;
        }

        StringBuilder stringBuilder = new();
        for (int i = 0; i < this.rolls.Count; i++)
        {
            stringBuilder.Append(this.rolls[i].ToString());
            if (i < this.rolls.Count - 1)
            {
                stringBuilder.Append(' ');
            }
        }

        this.toStringResult = stringBuilder.ToString();
        return this.toStringResult;
    }

    public bool Equals(IRoll? other)
    {
        if (other is not IMultipleRoll multipleRoll)
        {
            return false;
        }

        return Equals(multipleRoll);
    }

    public bool Equals(IMultipleRoll? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Enumerable.SequenceEqual(Rolls, other.Rolls);
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        this.rolls.ForEach(hash.Add);

        return hash.ToHashCode();
    }
}
