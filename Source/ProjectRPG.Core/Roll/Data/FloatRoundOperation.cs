namespace ProjectRPG.Core;

public enum FloatRoundOperation
{
    Round,
    Floor,
    Ceil,
}

internal static class FloatRoundOperationExtensions
{

    public static int ApplyOnDouble(this FloatRoundOperation operation, double value)
    {
        return (int)(operation switch
        {
            FloatRoundOperation.Ceil => Math.Ceiling(value),
            FloatRoundOperation.Floor => Math.Floor(value),
            _ => Math.Round(value)
        });
    }

}
