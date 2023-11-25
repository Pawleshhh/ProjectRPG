namespace ProjectRPG.Core;

public interface ICopyableAndModifiable<TCopy, TModifiable> : ICopyable<TCopy>
    where TCopy : notnull
    where TModifiable : notnull
{

    public TCopy CopyAndModify(Func<TModifiable, TCopy> modify);

}
