namespace ProjectRPG.Core.Test;

internal abstract class RollBaseTest<T> where T : IRoll, new()
{

    protected T CreateRoll()
        => CreateRoll(MoqHelper.CreateRngMock(0));

    protected T CreateRoll(Mock<IRandomNumberGenerator> rngMock)
        => new() { Rng = rngMock.Object };

}
