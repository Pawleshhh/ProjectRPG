namespace ProjectRPG.Core.Test;

[TestFixture]
internal class MultipleRollBaseTest : RollBaseTest<MultipleRollBase>
{

    [Test]
    public void AddRolls_AddsGivenRolls_RollsContainsThemAndReturnsItself()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock(),
            new RollMock(),
            new RollMock(),
        };
        var multipleRoll = CreateRoll();

        // ACT
        var itself = multipleRoll.AddRolls(rolls);

        // ASSERT
        Assert.Multiple(() =>
        {
            AssertSameAs(0);
            AssertSameAs(1);
            AssertSameAs(2);

            Assert.That(itself, Is.SameAs(multipleRoll));

            void AssertSameAs(int index)
                => Assert.That(multipleRoll.Rolls.ElementAt(index), Is.SameAs(rolls[index]));
        });
    }

    [Test]
    public void ClearRolls_ClearsRolls_RollsHasNoItemsAndReturnsItself()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock(),
            new RollMock(),
            new RollMock(),
        };
        var multipleRoll = CreateRoll();

        // ACT
        var itself = multipleRoll.ClearRolls();

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(multipleRoll.Rolls, Is.Empty);
            Assert.That(itself, Is.SameAs(multipleRoll));
        });
    }

    [Test]
    public void ToString_AddRolls_ReturnsBuiltString()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock() { ToStringResult = "A" },
            new RollMock() { ToStringResult = "B" },
            new RollMock() { ToStringResult = "C" },
        };
        var multipleRoll = CreateRoll()
            .AddRolls(rolls);

        // ACT
        var result = multipleRoll.ToString();

        // ASSERT
        Assert.That(result, Is.EqualTo("A, B, C"));
    }

    [Test]
    public void Roll_RollsAll_CallsAllRollsAndSumsResult()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            CreateMockRoll(5),
            CreateMockRoll(3),
            CreateMockRoll(2)
        };
        var multipleRoll = CreateRoll()
            .AddRolls(rolls);

        // ACT
        var result = multipleRoll.Roll();

        // ASSERT
        Assert.That(result, Is.EqualTo(10));

        IRoll CreateMockRoll(int result)
        {
            var mock = new Mock<IRoll>();
            mock.Setup(x => x.Roll()).Returns(result);
            return mock.Object;
        }
    }

    [Test]
    public void Equals_NullValue_ReturnsFalse()
    {
        // ARRANGE
        var roll = CreateRoll();

        // ACT & ASSERT
        TestEquatable.TestEquals_NullValue_ReturnsFalse<IMultipleRoll>(roll);
    }

    [Test]
    public void Equals_SameReference_ReturnsTrue()
    {
        // ARRANGE
        var roll = CreateRoll();

        // ACT & ASSERT
        TestEquatable.TestEquals_SameReference_ReturnsTrue<IMultipleRoll>(roll);
    }

    [Test]
    public void Equals_EqualValue_ReturnsTrue()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 3 },
        };
        var otherRolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 3 },
        };
        var roll = CreateRoll().AddRolls(rolls);
        var otherRoll = CreateRoll().AddRolls(otherRolls);

        // ACT & ASSERT
        TestEquatable.TestEquals_EqualValue_ReturnsTrue(roll, otherRoll);
    }

    [Test]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 3 },
        };
        var otherRolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 4 },
        };
        var roll = CreateRoll().AddRolls(rolls);
        var otherRoll = CreateRoll().AddRolls(otherRolls);

        // ACT & ASSERT
        TestEquatable.TestEquals_DifferentValue_ReturnsFalse(roll, otherRoll);
    }

    [Test]
    public void Equals_NotExpectedType_ReturnsFalse()
    {
        // ARRANGE
        var roll = CreateRoll();
        IRoll? other = new RollMock();

        // ACT
        var result = roll.Equals(other);

        // ASSERT
        Assert.That(result, Is.False);
    }

    [Test]
    public void ObjectEquals_NullValue_ReturnsFalse()
    {
        // ARRANGE
        var roll = CreateRoll();

        // ACT & ASSERT
        TestEquatable.TestObjectEquals_NullValue_ReturnsFalse<IMultipleRoll>(roll);
    }

    [Test]
    public void ObjectEquals_SameReference_ReturnsTrue()
    {
        // ARRANGE
        var roll = CreateRoll();

        // ACT & ASSERT
        TestEquatable.TestObjectEquals_SameReference_ReturnsTrue<IMultipleRoll>(roll);
    }

    [Test]
    public void ObjectEquals_EqualValue_ReturnsTrue()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 3 },
        };
        var otherRolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 3 },
        };
        var roll = CreateRoll().AddRolls(rolls);
        var otherRoll = CreateRoll().AddRolls(otherRolls);

        // ACT & ASSERT
        TestEquatable.TestObjectEquals_EqualValue_ReturnsTrue(roll, otherRoll);
    }

    [Test]
    public void ObjectEquals_DifferentValue_ReturnsFalse()
    {
        // ARRANGE
        var rolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 3 },
        };
        var otherRolls = new IRoll[]
        {
            new RollMock() { IdForEquals = 1 },
            new RollMock() { IdForEquals = 2 },
            new RollMock() { IdForEquals = 4 },
        };
        var roll = CreateRoll().AddRolls(rolls);
        var otherRoll = CreateRoll().AddRolls(otherRolls);

        // ACT & ASSERT
        TestEquatable.TestObjectEquals_DifferentValue_ReturnsFalse(roll, otherRoll);
    }

    [Test]
    public void ObjectEquals_NotExpectedType_ReturnsFalse()
    {
        // ARRANGE
        var roll = CreateRoll();
        IRoll? other = new RollMock();

        // ACT
        var result = roll.Equals(other);

        // ASSERT
        Assert.That(result, Is.False);
    }

}
