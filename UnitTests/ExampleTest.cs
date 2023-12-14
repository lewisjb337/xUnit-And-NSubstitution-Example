using ExampleProject;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using static ExampleProject.ExampleClass;

namespace UnitTests;

public class ExampleTest
{
    private ExampleClass _sut;
    private ILogger<ExampleClass> _loggerMock = Substitute.For<ILogger<ExampleClass>>();

    public ExampleTest()
    {
        _sut = new ExampleClass(_loggerMock);
    }

    // This should fail test
    [Fact]
    public void MultiplyValues_ShouldLogCriticalAndThrow_WhenNullEntityProvided()
    {
        // Arrange
        Example entity = null;

        var expectedMessage = "Failed to multiply 0 by 0";

        // Act
        var resultAction = () => _sut.MultiplyValues(entity);

        // Assert
        resultAction.Should().Throw<NullReferenceException>();
        
        _loggerMock.Received().Log(LogLevel.Critical, expectedMessage);
    }

    // This should pass test
    [Theory]
    [InlineData(5, 20, 100)]
    [InlineData(2, 31, 62)]
    [InlineData(6, 8, 48)]
    public void MultiplyValues_ShouldReturnCorrectResult_WhenTestCaseCorrect(int valueOne, int valueTwo, int result)
    {
        // Arrange
        var testCase = new Example
        {
            ValueOne = valueOne,
            ValueTwo = valueTwo
        };

        var expectedOutput = new Example
        {
            Result = result
        };

        // Act
        var results = _sut.MultiplyValues(testCase);

        // Assert
        results.Should().Be(expectedOutput.Result);
    }
}
