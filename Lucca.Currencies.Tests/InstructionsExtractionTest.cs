using Lucca.Currencies.Helpers;
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
        Assert.Equal(6, instructions.Iterations);
        Assert.Equal(6, instructions.Nodes.Count);
        Assert.True(instructions.ExchangeRates.ContainsKey("AUD_JPY"));
        Assert.Equal((decimal)86.0305, instructions.ExchangeRates["AUD_JPY"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("JPY_AUD"));
        Assert.Equal(Math.Round(1 / (decimal)86.0305, 4), instructions.ExchangeRates["JPY_AUD"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("AUD_CHF"));
        Assert.Equal((decimal)0.9661, instructions.ExchangeRates["AUD_CHF"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("CHF_AUD"));
        Assert.Equal(Math.Round(1 / (decimal)0.9661, 4), instructions.ExchangeRates["CHF_AUD"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("JPY_INR"));
        Assert.Equal((decimal)0.6571, instructions.ExchangeRates["JPY_INR"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("INR_JPY"));
        Assert.Equal(Math.Round(1 / (decimal)0.6571, 4), instructions.ExchangeRates["INR_JPY"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("EUR_CHF"));
        Assert.Equal((decimal)1.2053, instructions.ExchangeRates["EUR_CHF"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("CHF_EUR"));
        Assert.Equal(Math.Round(1 / (decimal)1.2053, 4), instructions.ExchangeRates["CHF_EUR"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("EUR_USD"));
        Assert.Equal((decimal)1.2989, instructions.ExchangeRates["EUR_USD"]);
        Assert.True(instructions.ExchangeRates.ContainsKey("USD_EUR"));
        Assert.Equal(Math.Round(1 / (decimal)1.2989, 4), instructions.ExchangeRates["USD_EUR"]);

        Assert.Contains("EUR", instructions.Vertices);
        Assert.Contains("JPY", instructions.Vertices);
        Assert.Contains("AUD", instructions.Vertices);
        Assert.Contains("CHF", instructions.Vertices);
        Assert.Contains("JPY", instructions.Vertices);
        Assert.Contains("KRW", instructions.Vertices);
        Assert.Contains("USD", instructions.Vertices);
        Assert.Contains("EUR", instructions.Vertices);
        Assert.Contains("INR", instructions.Vertices);
    }

    [Fact]
    public void TestBadInstructionExtractions()
    {
        string filePath = "BadInstructionsFile.txt";
        IInstructionsExtraction _extraction = new InstructionsExtraction();

        Assert.Throws<CurrencyException>(() => _extraction.ExtractInstructions(filePath));
    }
}