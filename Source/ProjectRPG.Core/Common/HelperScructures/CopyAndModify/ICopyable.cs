namespace ProjectRPG.Core;

public interface ICopyable<T> : ICloneable
    where T : notnull
{

    public T Copy();

    object ICloneable.Clone() => Clone();

}
