namespace ProjectRPG.Core.Test;

[TestFixture]
internal class DiceTest
{

    [TestCase(4  , "D4")]
    [TestCase(6  , "D6")]
    [TestCase(8  , "D8")]
    [TestCase(10 , "D10")]
    [TestCase(12 , "D12")]
    [TestCase(20 , "D20")]
    [TestCase(100, "D100")]
    public void ToString_GetDiceString_ReturnsExpectedString(int sides, string diceString)
    {
        // ARRANGE
        var dice = new Dice(sides);

        // ACT
        var result = dice.ToString();

        // ASSERT
        Assert.That(result, Is.EqualTo(diceString));
    }

}
