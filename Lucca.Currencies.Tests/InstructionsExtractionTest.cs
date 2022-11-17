using Lucca.Currencies.Services.Abstractions;

namespace Lucca.Currencies.Tests;


public class InstructionsExtractionTest
{
    [Fact]
    public void TestSimpleInstructionExtractions()
    {
        string filePath = "SimpleInstructionsFile.txt";
        IInstructionsExtraction _extraction = new InstructionsExtraction();

        var instructions = _extraction.ExtractInstructions(filePath);

        Assert.NotNull(instructions);
        Assert.Equal("EUR", instructions.StartCurrency);
        Assert.Equal("JPY", instructions.EndCurrency);
        Assert.Equal(550, instructions.Value);
    }
}