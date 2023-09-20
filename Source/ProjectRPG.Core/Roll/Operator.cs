using System.Numerics;

namespace ProjectRPG.Core;

public enum Operator
{
    Add,
    Substract,
    Multiply,
    Divide,
}

internal static class OperatorExtensions
{
    public static char GetOpCharacter(this Operator op)
    {
        return op switch
        { 
            Operator.Add => '+',
            Operator.Substract => '-',
            Operator.Multiply => 'x',
            Operator.Divide => '/',
            _ => throw GetNotDefinedOperatorException()
        };
    }

    public static T ApplyOp<T>(this Operator op, T a, T b)
        where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>,
                    IMultiplyOperators<T, T, T>, IDivisionOperators<T, T, T>
    {
        return op switch
        {
            Operator.Add => a + b,
            Operator.Substract => a - b,
            Operator.Multiply => a * b,
            Operator.Divide => a / b,
            _ => throw GetNotDefinedOperatorException()
        };
    }

    public static ArgumentException GetNotDefinedOperatorException()
        => new ArgumentException("There is not such operator defined");

}