using System.Collections.Immutable;

namespace ProjectRPG.Core;

public class CharacterDescriptor : 
    IEquatable<CharacterDescriptor>,
    ICopyableAndModifiable<CharacterDescriptor, Dictionary<string, ICharacterInfo>>
{

    #region Constructors

    public CharacterDescriptor(IEnumerable<ICharacterInfo> characterInfos)
    {
        CharacterInfos = characterInfos.ToDictionary(info => info.Key);
    }

    public CharacterDescriptor(IDictionary<string, ICharacterInfo> characterInfos)
    {
        CharacterInfos = characterInfos.ToImmutableDictionary();
    }

    #endregion

    #region Properties

    public IReadOnlyDictionary<string, ICharacterInfo> CharacterInfos { get; }

    #endregion

    #region Methods

    public CharacterDescriptor Copy()
    {
        return new CharacterDescriptor(CopyCharacterInfos());
    }

    public CharacterDescriptor CopyAndModify(Func<Dictionary<string, ICharacterInfo>, CharacterDescriptor> modify)
    {
        return modify(CopyCharacterInfos());
    }

    private Dictionary<string, ICharacterInfo> CopyCharacterInfos()
        => CharacterInfos.Values.Select(c => c.Copy()).ToDictionary(c => c.Key);

    #endregion

    #region IEquatable

    public bool Equals(CharacterDescriptor? other)
    {
        return this.Equals(other, () =>
        {
            return CharacterInfos.IsEqualTo(other!.CharacterInfos);
        });
    }

    public override bool Equals(object? obj)
    {
        return this.ObjectEquals(obj);
    }

    public override int GetHashCode()
    {
        return CharacterInfos
            .Sum(info => info.GetHashCode())
            .GetHashCode();
    }

    #endregion

}
