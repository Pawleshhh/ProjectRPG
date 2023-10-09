namespace ProjectRPG.Core;

public class CharacterDescriptor : IEquatable<CharacterDescriptor>
{

    #region Constructors

    public CharacterDescriptor(IEnumerable<ICharacterInfo> characterInfos)
    {
        CharacterInfos = characterInfos.ToDictionary(info => info.Key);
    }

    #endregion

    #region Properties

    public IReadOnlyDictionary<string, ICharacterInfo> CharacterInfos { get; }

    #endregion

    #region IEquatable

    public bool Equals(CharacterDescriptor? other)
    {
        return this.Equals(other, () =>
        {
            return false;
        });
    }

    #endregion

}
