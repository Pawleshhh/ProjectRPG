namespace ProjectRPG.Core.Test;

internal class RollMock : IRoll
{
    public IRandomNumberGenerator Rng { get; init; } = default!;

    public string? ToStringResult { get; set; }

    public int IdForEquals { get; set; }

    public bool Equals(IRoll? other)
    {
        return IdForEquals == (other as RollMock)!.IdForEquals;
    }

    public int Roll()
    {
        return 0;
    }

    public override string ToString()
    {
        return ToStringResult ?? string.Empty;
    }
}
