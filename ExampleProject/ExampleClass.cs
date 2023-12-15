using Microsoft.Extensions.Logging;

namespace ExampleProject;

public class ExampleClass : IExampleClass
{
    private readonly ILogger<ExampleClass> _logger;

    public ExampleClass(ILogger<ExampleClass> logger)
    {
        _logger = logger;
    }

    public int MultiplyValues(Example data)
    {
        try
        {
            data.Result = data.ValueOne * data.ValueTwo;

            return data.Result;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, $"Failed to multiply values");
            throw;
        }
    }

    public class Example
    {
        public int ValueOne { get; set; }
        public int ValueTwo { get; set; }
        public int Result { get; set; }
    }
}