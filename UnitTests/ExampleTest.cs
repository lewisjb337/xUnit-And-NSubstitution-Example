using ExampleProject;
using FluentAssertions;
using NSubstitute;
using Xunit;
using static ExampleProject.ExampleClass;

namespace UnitTests;

public class ExampleTest
{
    private IExampleClass _sut = Substitute.For<IExampleClass>();

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