using System.Collections;

namespace ProjectRPG.Core;

internal static class HashCodeHelper
{

    public static int GetHashCodeOfCollection(IEnumerable collection)
    {
        int hc = 0;
        foreach (var item in collection) 
        { 
            hc ^= item.GetHashCode();
        }
        return hc;
    }

}
