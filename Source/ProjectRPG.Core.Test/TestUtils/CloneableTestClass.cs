namespace ProjectRPG.Core.Test;

internal class CloneableTestClass : ICloneable
{
    public object? Data { get; set; }

    public object Clone()
    {
        return new CloneableTestClass { Data = this.Data };
    }
}
