namespace ProjectRPG.Core.Test;

internal static class MoqHelper
{

    public static Mock<IRandomNumberGenerator> CreateRngMock(int result)
    {
        var mock = new Mock<IRandomNumberGenerator>();
        mock.Setup(x => x.NextInt()).Returns(result);
        mock.Setup(x => x.NextInt(It.IsAny<int>())).Returns(result);
        mock.Setup(x => x.NextInt(It.IsAny<int>(), It.IsAny<int>())).Returns(result);
        return mock;
    }

    public static Mock<IRandomNumberGenerator> CreateRngMock(params int[] results)
    {
        return CreateRngMock(false, results);
    }

    public static Mock<IRandomNumberGenerator> CreateRngMock(bool repeat, params int[] results)
    {
        int index = 0;
        int length = results.Length;

        var mock = new Mock<IRandomNumberGenerator>();
        mock.Setup(x => x.NextInt()).Returns(Returns);
        mock.Setup(x => x.NextInt(It.IsAny<int>())).Returns(Returns);
        mock.Setup(x => x.NextInt(It.IsAny<int>(), It.IsAny<int>())).Returns(Returns);

        return mock;

        int Returns()
        {
            if (index >= length && repeat)
            {
                index = 0;
            }

            return results[index++];
        }
    }

}
