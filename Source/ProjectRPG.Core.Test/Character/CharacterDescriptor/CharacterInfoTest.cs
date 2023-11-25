namespace ProjectRPG.Core.Test;

[TestFixture]
internal class CharacterInfoTest
{
    [Test]
    public void CreateStringInfo_ShouldReturnCorrectObject()
    {
        // Arrange
        string key = "Name";
        string value = "John";

        // Act
        var stringInfo = CharacterInfo<string>.CreateStringInfo(key, value);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(stringInfo.Key, Is.EqualTo(key));
            Assert.That(stringInfo.Value, Is.EqualTo(value));
            Assert.That(stringInfo.CharacterInfoType, Is.EqualTo(CharacterInfoType.String));
        });
    }

    [Test]
    public void CreateIntInfo_ShouldReturnCorrectObject()
    {
        // Arrange
        string key = "Level";
        int value = 5;

        // Act
        var intInfo = CharacterInfo<int>.CreateIntInfo(key, value);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(intInfo.Key, Is.EqualTo(key));
            Assert.That(intInfo.Value, Is.EqualTo(value));
            Assert.That(intInfo.CharacterInfoType, Is.EqualTo(CharacterInfoType.Int));
        });
    }

    [Test]
    public void CreateDoubleInfo_ShouldReturnCorrectObject()
    {
        // Arrange
        string key = "Health";
        double value = 75.5;

        // Act
        var doubleInfo = CharacterInfo<double>.CreateDoubleInfo(key, value);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(doubleInfo.Key, Is.EqualTo(key));
            Assert.That(doubleInfo.Value, Is.EqualTo(value));
            Assert.That(doubleInfo.CharacterInfoType, Is.EqualTo(CharacterInfoType.Double));
        });
    }

    [Test]
    public void Copy_ShouldReturnClonedObject()
    {
        // Arrange
        var originalInfo = CharacterInfo<int>.CreateIntInfo("Test", 42);

        // Act
        var clonedInfo = originalInfo.Copy();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(clonedInfo, Is.Not.SameAs(originalInfo));
            Assert.That(clonedInfo.Key, Is.EqualTo(originalInfo.Key));
            Assert.That(clonedInfo.Value, Is.EqualTo(originalInfo.Value));
            Assert.That(clonedInfo.CharacterInfoType, Is.EqualTo(originalInfo.CharacterInfoType));
        });
    }

    [Test]
    public void Copy_ShouldReturnClonedObject_WhenValueIsICloneable()
    {
        // Arrange
        var cloneableValue = new CloneableTestClass { Data = "Test Data" };
        var originalInfo = new CharacterInfo<CloneableTestClass>()
        {
            Key = "CloneableTest",
            Value = cloneableValue,
            CharacterInfoType = CharacterInfoType.Other
        };

        // Act
        var clonedInfo = (CharacterInfo<CloneableTestClass>)originalInfo.Copy();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(clonedInfo, Is.Not.SameAs(originalInfo));
            Assert.That(clonedInfo.Key, Is.EqualTo(originalInfo.Key));
            Assert.That(clonedInfo.Value.Data, Is.EqualTo(originalInfo.Value.Data));
            Assert.That(clonedInfo.CharacterInfoType, Is.EqualTo(originalInfo.CharacterInfoType));
        });
    }

    [Test]
    public void ToString_ShouldReturnCorrectStringRepresentation()
    {
        // Arrange
        var doubleInfo = CharacterInfo<double>.CreateDoubleInfo("Height", 180.7523);

        // Act
        string result = doubleInfo.ToString();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo("Height : 180.75"));
        });
    }
}
