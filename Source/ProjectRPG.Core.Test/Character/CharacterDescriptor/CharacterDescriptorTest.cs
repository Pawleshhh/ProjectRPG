namespace ProjectRPG.Core.Test;

[TestFixture]
internal class CharacterDescriptorTest
{

    [Test]
    public void Constructor_WithIEnumerable_ShouldInitializeCharacterInfos()
    {
        // Arrange
        var characterInfos = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };

        // Act
        var characterDescriptor = new CharacterDescriptor(characterInfos);

        // Assert
        CollectionAssert.AreEquivalent(characterInfos.ToDictionary(info => info.Key), characterDescriptor.CharacterInfos);
    }

    [Test]
    public void Constructor_WithIDictionary_ShouldInitializeCharacterInfos()
    {
        // Arrange
        var characterInfos = new Dictionary<string, ICharacterInfo>
        {
            { "Name", CharacterInfo<string>.CreateStringInfo("Name", "John") },
            { "Level", CharacterInfo<int>.CreateIntInfo("Level", 5) }
        };

        // Act
        var characterDescriptor = new CharacterDescriptor(characterInfos);

        // Assert
        CollectionAssert.AreEquivalent(characterInfos, characterDescriptor.CharacterInfos);
    }

    [Test]
    public void Copy_ShouldReturnClonedCharacterDescriptor()
    {
        // Arrange
        var characterInfos = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var originalDescriptor = new CharacterDescriptor(characterInfos);

        // Act
        var clonedDescriptor = originalDescriptor.Copy();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(clonedDescriptor, Is.Not.SameAs(originalDescriptor));
            CollectionAssert.AreEquivalent(originalDescriptor.CharacterInfos, clonedDescriptor.CharacterInfos);
        });
    }

    [Test]
    public void CopyAndModify_ShouldReturnModifiedCharacterDescriptor()
    {
        // Arrange
        var characterInfos = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var originalDescriptor = new CharacterDescriptor(characterInfos);

        // Act
        var modifiedDescriptor = originalDescriptor.CopyAndModify(dictionary =>
        {
            dictionary["Level"] = CharacterInfo<int>.CreateIntInfo("Level", 10);
            return new CharacterDescriptor(dictionary);
        });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modifiedDescriptor, Is.Not.SameAs(originalDescriptor));
            Assert.That(modifiedDescriptor.CharacterInfos["Level"].AsInt(), Is.Not.EqualTo(originalDescriptor.CharacterInfos["Level"].AsInt()));
            Assert.That(modifiedDescriptor.CharacterInfos["Level"].AsInt(), Is.EqualTo(10));
        });
    }

    [Test]
    public void Equals_ShouldReturnTrue_WhenCharacterInfosAreEqual()
    {
        // Arrange
        var characterInfos1 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var characterInfos2 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var descriptor1 = new CharacterDescriptor(characterInfos1);
        var descriptor2 = new CharacterDescriptor(characterInfos2);

        // Act & Assert
        TestEquatable.TestEquals_EqualValue_ReturnsTrue(descriptor1, descriptor2);
    }

    [Test]
    public void Equals_ShouldReturnFalse_WhenCharacterInfosAreNotEqual()
    {
        // Arrange
        var characterInfos1 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var characterInfos2 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "Jane"), // Different value for "Name"
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var descriptor1 = new CharacterDescriptor(characterInfos1);
        var descriptor2 = new CharacterDescriptor(characterInfos2);

        // Act & Assert
        TestEquatable.TestEquals_DifferentValue_ReturnsFalse(descriptor1, descriptor2);
    }

    [Test]
    public void Equals_ShouldReturnTrue_WhenCharacterInfosAreSameReference()
    {
        // Arrange
        var characterInfos = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var descriptor = new CharacterDescriptor(characterInfos);

        // Act & Assert
        TestEquatable.TestEquals_SameReference_ReturnsTrue(descriptor);
    }

    [Test]
    public void GetHashCode_ShouldReturnSameHashCode_WhenCharacterInfosAreEqual()
    {
        // Arrange
        var characterInfos1 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var characterInfos2 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var descriptor1 = new CharacterDescriptor(characterInfos1);
        var descriptor2 = new CharacterDescriptor(characterInfos2);

        // Act
        var hashCode1 = descriptor1.GetHashCode();
        var hashCode2 = descriptor2.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.EqualTo(hashCode1));
    }

    [Test]
    public void GetHashCode_ShouldReturnDifferentHashCode_WhenCharacterInfosAreNotEqual()
    {
        // Arrange
        var characterInfos1 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var characterInfos2 = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "Jane"), // Different value for "Name"
            CharacterInfo<int>.CreateIntInfo("Level", 5)
        };
        var descriptor1 = new CharacterDescriptor(characterInfos1);
        var descriptor2 = new CharacterDescriptor(characterInfos2);

        // Act
        var hashCode1 = descriptor1.GetHashCode();
        var hashCode2 = descriptor2.GetHashCode();

        // Assert
        Assert.That(hashCode2, Is.Not.EqualTo(hashCode1));
    }

    [Test]
    public void ToString_ShouldReturnCorrectStringRepresentation()
    {
        // Arrange
        var characterInfos = new List<ICharacterInfo>
        {
            CharacterInfo<string>.CreateStringInfo("Name", "John"),
            CharacterInfo<int>.CreateIntInfo("Level", 5),
            CharacterInfo<double>.CreateDoubleInfo("Money", 12.4321)
        };

        var descriptor = new CharacterDescriptor(characterInfos);
        var newLine = Environment.NewLine;
        var expectedString = $"Name : John{newLine}Level : 5{newLine}Money : 12.43{newLine}";

        // Act
        var result = descriptor.ToString();

        // Assert
        Assert.That(result, Is.EqualTo(expectedString));
    }

}
