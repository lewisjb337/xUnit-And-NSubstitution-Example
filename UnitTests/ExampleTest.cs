using ExampleProject;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit.Abstractions;
using static ExampleProject.ExampleClass;
using ExampleProject.Extensions;

namespace UnitTests;

/// <summary>
/// In order to run a test, right click on a test name and click 'Run Tests', this should display a menu where all test cases can be seen.
/// Tests that pass will show a green tick and those that failed will show a red cross, you can run tests manually or click the 'Debug'
/// option to run tests that allow breakpoints within the tests to be hit.
/// </summary>

// If we need to group tests together, we can do so with the Trait attribute
[Trait("Name", "Category")]
public class ExampleTest : IDisposable
{
    private ExampleClass _sut;
    private ILogger<ExampleClass> _loggerMock = Substitute.For<ILogger<ExampleClass>>();

    private readonly ITestOutputHelper _outputHelper;

    public ExampleTest(ITestOutputHelper outputHelper)
    {
        _sut = new ExampleClass(_loggerMock);
        _outputHelper = outputHelper;
    }

    [Fact]
    public void MultiplyValues_ShouldLogCriticalAndThrow_WhenNullListProvided()
    {
        // Arrange
        Example entity = null;

        var expectedMessage = "Failed to multiply values";

        // Act
        var resultAction = () => _sut.MultiplyValues(entity);

        // Assert
        resultAction.Should()
            .Throw<NullReferenceException>();

        _loggerMock.VerifyLogged(LogLevel.Critical, expectedMessage, typeof(NullReferenceException));
    }

    // If we want to add multiple tests, we can do so using the InlineData attribute
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

        // Here is an example of creating outputs during tests
        _outputHelper.WriteLine($"Output should be {result}");

        // Assert
        results.Should().Be(expectedOutput.Result);
    }

    // If we want to skip over a test, we can do so using the following attribute
    [Fact(Skip = "This test is skipped")]
    public void SkippedTest() {}

    public void Dispose()
    {
        // Cleanup
    }
}