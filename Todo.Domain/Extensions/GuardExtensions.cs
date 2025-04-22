using Ardalis.GuardClauses;

namespace Todo.Domain.Extensions;

public static class GuardExtensions
{
    public static void NotEqualTo<T>(this IGuardClause _, T input, T expected, string parameterName)
    {
        if (!EqualityComparer<T>.Default.Equals(input, expected))
        {
            throw new ArgumentException($"Value must be equal to {expected}.", parameterName);
        }
    }
}
