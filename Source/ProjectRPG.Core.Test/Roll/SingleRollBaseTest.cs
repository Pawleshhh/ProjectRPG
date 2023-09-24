namespace ProjectRPG.Core.Test;

[TestFixture]
internal class SingleRollBaseTest
{

    [TestCase(4)]
    [TestCase(6)]
    [TestCase(8)]
    [TestCase(10)]
    [TestCase(12)]
    [TestCase(20)]
    [TestCase(100)]
    public void SetDice_SetsExpectedDice_ReturnsItself(int diceSides)
    {
        SetDiceTest((r, d) => r.SetDice(d), diceSides);
    }

    [TestCase(4, 1)]
    [TestCase(6, 2)]
    [TestCase(8, 3)]
    [TestCase(10, 4)]
    [TestCase(12, 5)]
    [TestCase(20, 6)]
    [TestCase(100, 7)]
    public void SetDice_SetsExpectedDiceWithProperCount_ReturnsItself(int diceSides, int count)
    {
        Assert.Multiple(() =>
        {
            var roll = SetDiceTest((r, d) => r.SetDice(d, count), diceSides);
            Assert.That(roll.DiceCount, Is.EqualTo(count));
        });
    }

    [TestCase(4, -1)]
    [TestCase(6, -2)]
    [TestCase(8, -3)]
    [TestCase(10, -4)]
    [TestCase(12, -5)]
    [TestCase(20, -6)]
    [TestCase(100, -7)]
    public void SetDice_SetsNegativeDiceCount_ThrowsArgumentOutOfRangeException(int diceSides, int count)
    {
        // ARRANGE
        var roll = CreateRoll();

        // ACT & ASSERT
        Assert.Throws<ArgumentOutOfRangeException>(() => roll.SetDice(new(diceSides), count));
    }

    [TestCase(5, Operator.Add)]
    [TestCase(-3, Operator.Substract)]
    [TestCase(-11, Operator.Multiply)]
    [TestCase(17, Operator.Divide)]
    public void SetFactor_SetsFactorWithOperator_ReturnsItself(int factor, Operator op)
    {
        SetFactorTest((r, f, o) => r.SetFactor(f, o), factor, op);
    }

    [TestCase(5, Operator.Add, FloatRoundOperation.Round)]
    [TestCase(5, Operator.Add, FloatRoundOperation.Ceil)]
    [TestCase(5, Operator.Add, FloatRoundOperation.Floor)]
    [TestCase(-3, Operator.Substract, FloatRoundOperation.Round)]
    [TestCase(-3, Operator.Substract, FloatRoundOperation.Ceil)]
    [TestCase(-3, Operator.Substract, FloatRoundOperation.Floor)]
    [TestCase(-11, Operator.Multiply, FloatRoundOperation.Round)]
    [TestCase(-11, Operator.Multiply, FloatRoundOperation.Ceil)]
    [TestCase(-11, Operator.Multiply, FloatRoundOperation.Floor)]
    [TestCase(17, Operator.Divide, FloatRoundOperation.Round)]
    [TestCase(17, Operator.Divide, FloatRoundOperation.Ceil)]
    [TestCase(17, Operator.Divide, FloatRoundOperation.Floor)]
    public void SetFactor_SetsFactorWithOperatorAndRoundOperator_ReturnsItself(int factor, Operator op, FloatRoundOperation roundOp)
    {
        Assert.Multiple(() =>
        {
            var roll = SetFactorTest((r, f, o) => r.SetFactor(f, o, roundOp), factor, op);
            Assert.That(roll.FloatRoundOperation, Is.EqualTo(roundOp));
        });
    }

    [TestCase(5, 1   , Operator.Add      ,  10, "D5 + 10")]
    [TestCase(5, 11  , Operator.Substract,  10, "11D5 - 10")]
    [TestCase(5, 4   , Operator.Multiply ,  10, "4D5 x 10")]
    [TestCase(5, 6   , Operator.Divide   ,  10, "6D5 / 10")]
    [TestCase(5, 9   , Operator.Add      , -17, "9D5 + (-17)")]
    [TestCase(5, 3   , Operator.Substract, -17, "3D5 - (-17)")]
    [TestCase(5, 2000, Operator.Multiply , -17, "2000D5 x (-17)")]
    [TestCase(5, 1   , Operator.Divide   , -17, "D5 / (-17)")]
    public void ToString_GetsExpectedString(int diceSides, int diceCount, Operator op, int factor, string expected)
    {
        // ARRANGE
        var roll = CreateRoll()
            .SetDice(new(diceSides), diceCount)
            .SetFactor(factor, op);

        // ACT
        var resultString = roll.ToString();

        // ASSERT
        Assert.That(resultString, Is.EqualTo(expected));
    }

    [TestCase(6, 6, 2, 2, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, true)]
    [TestCase(6, 5, 2, 2, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 3, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 2, 1, 0, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 2, 1, 1, Operator.Add, Operator.Substract, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 2, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Ceil, false)]
    public void Equals_OtherSingleRollWithGivenData_ReturnsTrueWhenEqual(
        int diceSides,
        int otherDiceSides,
        int diceCount,
        int otherDiceCount,
        int factor,
        int otherFactor,
        Operator @operator,
        Operator otherOperator,
        FloatRoundOperation roundOperation,
        FloatRoundOperation otherRoundOperation,
        bool expectedValue)
    {
        // ARRANGE
        var singleRoll = CreateSingleRoll(diceSides, diceCount, factor, @operator, roundOperation);
        var other = CreateSingleRoll(otherDiceSides, otherDiceCount, otherFactor, otherOperator, otherRoundOperation);

        ISingleRoll CreateSingleRoll(int diceSides, int diceCount, int factor, Operator op, FloatRoundOperation roundOp)
            => CreateRoll()
            .SetDice(new(diceSides), diceCount)
            .SetFactor(factor, op, roundOp);

        // ACT & ASSERT
        if (expectedValue)
        {
            TestEquatable.TestEquals_EqualValue_ReturnsTrue(singleRoll, other);
            return;
        }

        TestEquatable.TestEquals_DifferentValue_ReturnsFalse(singleRoll, other);
    }

    [Test]
    public void Equals_SameSingleRolls_ReturnsTrue()
    {
        // ARRANGE
        var singleRoll = CreateRoll();
        var otherRoll = singleRoll;

        // ACT & ASSERT
        TestEquatable.TestEquals_SameReference_ReturnsTrue<ISingleRoll>(singleRoll, otherRoll);
    }

    [Test]
    public void Equals_OtherSingleRollIsNull_ReturnsFalse()
    {
        // ARRANGE
        var singleRoll = CreateRoll();

        // ACT & ASSERT
        TestEquatable.TestEquals_NullValue_ReturnsFalse<ISingleRoll>(singleRoll);
    }

    [Test]
    public void Equals_OtherRollNotSingleRoll_ReturnsFalse()
    {
        // ARRANGE
        var singleRoll = CreateRoll();
        IRoll otherRoll = new RollMock();

        // ACT
        var result = singleRoll.Equals(otherRoll);

        // ASSERT
        Assert.That(result, Is.False);
    }

    [TestCase(6, 6, 2, 2, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, true)]
    [TestCase(6, 5, 2, 2, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 3, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 2, 1, 0, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 2, 1, 1, Operator.Add, Operator.Substract, FloatRoundOperation.Round, FloatRoundOperation.Round, false)]
    [TestCase(6, 6, 2, 2, 1, 1, Operator.Add, Operator.Add, FloatRoundOperation.Round, FloatRoundOperation.Ceil, false)]
    public void ObjectEquals_OtherSingleRollWithGivenData_ReturnsTrueWhenEqual(
        int diceSides,
        int otherDiceSides,
        int diceCount,
        int otherDiceCount,
        int factor,
        int otherFactor,
        Operator @operator,
        Operator otherOperator,
        FloatRoundOperation roundOperation,
        FloatRoundOperation otherRoundOperation,
        bool expectedValue)
    {
        // ARRANGE
        var singleRoll = CreateSingleRoll(diceSides, diceCount, factor, @operator, roundOperation);
        var other = CreateSingleRoll(otherDiceSides, otherDiceCount, otherFactor, otherOperator, otherRoundOperation);

        ISingleRoll CreateSingleRoll(int diceSides, int diceCount, int factor, Operator op, FloatRoundOperation roundOp)
            => CreateRoll()
            .SetDice(new(diceSides), diceCount)
            .SetFactor(factor, op, roundOp);

        // ACT & ASSERT
        if (expectedValue)
        {
            TestEquatable.TestObjectEquals_EqualValue_ReturnsTrue(singleRoll, other);
            return;
        }

        TestEquatable.TestObjectEquals_DifferentValue_ReturnsFalse(singleRoll, other);
    }

    [Test]
    public void ObjectEquals_SameSingleRolls_ReturnsTrue()
    {
        // ARRANGE
        var singleRoll = CreateRoll();
        var otherRoll = singleRoll;

        // ACT & ASSERT
        TestEquatable.TestObjectEquals_SameReference_ReturnsTrue<ISingleRoll>(singleRoll, otherRoll);
    }

    [Test]
    public void ObjectEquals_OtherSingleRollIsNull_ReturnsFalse()
    {
        // ARRANGE
        var singleRoll = CreateRoll();

        // ACT & ASSERT
        TestEquatable.TestObjectEquals_NullValue_ReturnsFalse<ISingleRoll>(singleRoll);
    }

    [TestCase(6, 1, 1, 2, 3, 1)]
    [TestCase(6, 2, 1, 2, 3, 3)]
    [TestCase(6, 3, 1, 2, 3, 6)]
    [TestCase(12, 1, 1, 2, 3, 1)]
    [TestCase(12, 2, 1, 2, 3, 3)]
    [TestCase(12, 3, 1, 2, 3, 6)]
    public void Roll_RollWithoutFactor_ReturnExpectedValue(int diceSides, int diceCount, int rng1, int rng2, int rng3, int expectedValue)
    {
        // ARRANGE
        var rngMock = MoqHelper.CreateRngMock(rng1, rng2, rng3);
        var roll = CreateRoll(rngMock)
            .SetDice(new(diceSides), diceCount);

        // ACT
        var result = roll.Roll();

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expectedValue));
            rngMock.Verify(x => x.NextInt(1, diceSides + 1), Times.Exactly(diceCount));
        });
    }

    [TestCase(2, Operator.Add, 8)]
    [TestCase(8, Operator.Substract, -2)]
    [TestCase(-3, Operator.Multiply, -18)]
    [TestCase(3, Operator.Divide, 2)]
    public void Roll_RollWithFactor_ReturnExpectedValue(int factor, Operator op, int expectedValue)
    {
        // ARRANGE
        var rngMock = MoqHelper.CreateRngMock(1, 2, 3);
        var roll = CreateRoll(rngMock)
            .SetDice(Dice.D6, 3)
            .SetFactor(factor, op);

        // ACT
        var result = roll.Roll();

        // ASSERT
        Assert.That(result, Is.EqualTo(expectedValue));
    }

    [TestCase(19, FloatRoundOperation.Round, 2)]
    [TestCase(15, FloatRoundOperation.Round, 2)]
    [TestCase(14, FloatRoundOperation.Round, 1)]
    [TestCase(19, FloatRoundOperation.Ceil, 2)]
    [TestCase(15, FloatRoundOperation.Ceil, 2)]
    [TestCase(14, FloatRoundOperation.Ceil, 2)]
    [TestCase(19, FloatRoundOperation.Floor, 1)]
    [TestCase(15, FloatRoundOperation.Floor, 1)]
    [TestCase(14, FloatRoundOperation.Floor, 1)]
    public void Roll_RollWithFactorAndResultIsDecimalAfterDivision_RoundOperatorApplied(int rngResult, FloatRoundOperation roundOp, int expectedValue)
    {
        // ARRANGE
        var rngMock = MoqHelper.CreateRngMock(rngResult);
        var roll = CreateRoll(rngMock)
            .SetDice(Dice.D20)
            .SetFactor(10, Operator.Divide, roundOp);

        // ACT
        var result = roll.Roll(); // rngResult / 10

        // ASSERT
        Assert.That(result, Is.EqualTo(expectedValue));
    }

    #region Reusable tests

    private ISingleRoll SetDiceTest(Func<SingleRollBase, Dice, ISingleRoll> act, int diceSides)
    {
        // ARRANGE
        Dice dice = new(diceSides);
        var roll = CreateRoll();

        // ACT
        var itself = act(roll, dice);

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(roll.Dice, Is.EqualTo(dice));
            Assert.That(itself, Is.SameAs(roll));
        });

        return itself;
    }

    private ISingleRoll SetFactorTest(Func<SingleRollBase, int, Operator, ISingleRoll> act, int factor, Operator op)
    {
        // ARRANGE
        var roll = CreateRoll();

        // ACT
        var itself = act(roll, factor, op);

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(roll.Factor, Is.EqualTo(factor));
            Assert.That(roll.FactorOperator, Is.EqualTo(op));
            Assert.That(itself, Is.SameAs(roll));
        });

        return itself;
    }

    #endregion

    #region Helpers

    private SingleRollBase CreateRoll()
        => CreateRoll(MoqHelper.CreateRngMock(0));

    private SingleRollBase CreateRoll(Mock<IRandomNumberGenerator> rngMock)
        => new SingleRollBase { Rng = rngMock.Object };

    #endregion

    #region Mocks

    class RollMock : IRoll
    {
        public IRandomNumberGenerator Rng { get; init; } = default!;

        public bool Equals(IRoll? other)
        {
            return false;
        }

        public int Roll()
        {
            return 0;
        }
    }

    #endregion

}
