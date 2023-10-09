namespace ProjectRPG.Core;

public interface ICharacterInfo
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