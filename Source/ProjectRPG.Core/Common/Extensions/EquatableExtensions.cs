namespace ProjectRPG.Core;

internal static class EquatableExtensions
{

    public static bool Equals<T>(this T @this, T? other, Func<bool> equals)
    {
        if (other is null)
        {
            return false;
        }

        return IsReferenceEqualOrEqualByValue(@this, other, equals);
    }

    public static bool ObjectEquals<T>(this T @this, object? obj)
        where T : IEquatable<T>
    {
        if (obj is T t)
        {
            return @this.Equals(t);
        }

        return false;
    }

    private static bool IsReferenceEqualOrEqualByValue<T>(T @this, T? other, Func<bool> equals)
    {
        if (ReferenceEquals(@this, other))
        {
            return true;
        }

        return equals();
    }

}
