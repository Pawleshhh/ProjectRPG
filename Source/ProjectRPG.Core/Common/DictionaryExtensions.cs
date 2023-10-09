namespace ProjectRPG.Core;

internal static class DictionaryExtensions
{

    public static bool IsEqualTo<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> other)
    {
        if (@this.Count != other.Count)
        {
            return false;
        }

        foreach (var key in @this.Keys)
        {
            if (!other.ContainsKey(key))
            {
                return false;
            }

            if (!@this[key]?.Equals(other[key]) == true)
            {
                return false;
            }
        }

        return true;
    }

}
