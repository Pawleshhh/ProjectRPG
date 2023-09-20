namespace ProjectRPG.Core;

public record Dice(int SideCount)
{

    public override string ToString()
    {
        return $"D{SideCount}";
    }

    #region Popular dices

    public static Dice D4 { get; } = new(4);
    public static Dice D6 { get; } = new(6);
    public static Dice D8 { get; } = new(8);
    public static Dice D10 { get; } = new(10);
    public static Dice D12 { get; } = new(12);
    public static Dice D20 { get; } = new(20);
    public static Dice D100 { get; } = new(100);

    #endregion

}
