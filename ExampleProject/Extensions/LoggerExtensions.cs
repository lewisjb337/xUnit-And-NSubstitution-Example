using Microsoft.Extensions.Logging;
using NSubstitute;

namespace ExampleProject.Extensions;

public static class LoggerExtensions
{
    public static void VerifyLogged<T>(this ILogger<T> logger, LogLevel level, string expectedMessage, Type? expectedExceptionType = null)
    {
        logger.Received().Log(
            level,
            Arg.Any<EventId>(),
            Arg.Is<object>(o => o.ToString()!.Contains(expectedMessage)),
            Arg.Is<Exception>(ex => expectedExceptionType == null || ex.GetType() == expectedExceptionType),
            Arg.Any<Func<object, Exception?, string>>());
    }
}