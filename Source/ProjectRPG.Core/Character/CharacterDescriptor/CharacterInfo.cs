namespace ProjectRPG.Core;

public interface ICharacterInfo : ICopyable<ICharacterInfo>
{

    public string Key { get; }

    public object Value { get; }

    public CharacterInfoType CharacterInfoType { get; }

    public string AsString() => (string)Value;
    public int AsInt() => (int)Value;
    public double AsDouble() => (double)Value;
    public T As<T>() => (T)Value;

}

public record CharacterInfo<T> : ICharacterInfo
{

    #region Properties

    public required string Key { get; init; }

    public required T Value { get; init; }

    public CharacterInfoType CharacterInfoType { get; init; } = CharacterInfoType.Other;

    object ICharacterInfo.Value => Value!;

    #endregion

    #region Clone

    public ICharacterInfo Copy()
    {
        if (Value is ICloneable cloneable)
        {
            return new CharacterInfo<T>()
            {
                Key = Key,
                Value = (T)cloneable.Clone(),
                CharacterInfoType = CharacterInfoType
            };
        }

        return (CharacterInfo<T>)MemberwiseClone();
    }

    #endregion

    #region ToString

    public override string ToString()
    {
        return CharacterInfoType switch
        {
            CharacterInfoType.Double => $"{Key} : {Value:F2}",
            _ => $"{Key} : {Value}"
        };
    }

    #endregion

}

public record CharacterInfo : CharacterInfo<object>
{

    #region Static methods

    public static CharacterInfo<string> CreateStringInfo(string key, string value)
        => new()
        {
            Key = key,
            Value = value,
            CharacterInfoType = CharacterInfoType.String
        };

    public static CharacterInfo<int> CreateIntInfo(string key, int value)
        => new()
        {
            Key = key,
            Value = value,
            CharacterInfoType = CharacterInfoType.Int
        };

    public static CharacterInfo<double> CreateDoubleInfo(string key, double value)
        => new()
        {
            Key = key,
            Value = value,
            CharacterInfoType = CharacterInfoType.Double
        };

    #endregion

}