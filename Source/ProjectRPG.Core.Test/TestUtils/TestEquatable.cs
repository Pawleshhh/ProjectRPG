namespace ProjectRPG.Core.Test;

internal static class TestEquatable
{

    #region Object.Equals

    public static void TestObjectEquals_NotExpectedType_ReturnsFalse<T>(T t)
        where T : IEquatable<T>
    {
        // ARRANGE
        object obj = new object();

        // ACT
        var result = t.Equals(obj);

        // ASSERT
        Assert.That(result, Is.False);
    }

    public static void TestObjectEquals_SameReference_ReturnsTrue<T>(T t, T same)
        where T : IEquatable<T>
    {
        // ACT
        var result = t.Equals((object)same);

        // ASSERT
        Assert.That(result, Is.True);
    }

    public static void TestObjectEquals_DifferentValue_ReturnsFalse<T>(T t, T other)
        where T : IEquatable<T>
    {
        // ACT
        var result = t.Equals((object)other);

        // ASSERT
        Assert.That(result, Is.False);
    }

    public static void TestObjectEquals_EqualValue_ReturnsTrue<T>(T t, T other)
        where T : IEquatable<T>
    {
        // ACT
        var result = t.Equals((object)other);

        // ASSERT
        Assert.That(result, Is.True);
    }

    public static void TestObjectEquals_NullValue_ReturnsFalse<T>(T t)
        where T : class, IEquatable<T>
    {
        // ARRANGE
        object? other = null;

        // ACT
        var result = t.Equals(other);

        // ASSERT
        Assert.That(result, Is.False);
    }

    #endregion Object.Equals

    #region IEquatable<T>.Equals

    public static void TestEquals_SameReference_ReturnsTrue<T>(T t, T same)
        where T : IEquatable<T>
    {
        // ACT
        var result = t.Equals(same);

        // ASSERT
        Assert.That(result, Is.True);
    }

    public static void TestEquals_DifferentValue_ReturnsFalse<T>(T t, T other)
        where T : IEquatable<T>
    {
        // ACT
        var result = t.Equals(other);

        // ASSERT
        Assert.That(result, Is.False);
    }

    public static void TestEquals_EqualValue_ReturnsTrue<T>(T t, T other)
        where T : IEquatable<T>
    {
        // ACT
        var result = t.Equals(other);

        // ASSERT
        Assert.That(result, Is.True);
    }

    public static void TestEquals_NullValue_ReturnsFalse<T>(T t)
        where T : class, IEquatable<T>
    {
        // ARRANGE
        T? other = null;

        // ACT
        var result = t.Equals(other);

        // ASSERT
        Assert.That(result, Is.False);
    }

    #endregion

}
