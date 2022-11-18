namespace Lucca.Currencies.Tests;

public class CurrencyConversionTest
{
    [Fact]
    public void TestConversions()
    {
        string filePath = "SimpleInstructionsFile.txt";
        IInstructionsExtraction _extraction = new InstructionsExtraction();
        ICurrencyConverter _currencyConverter = new CurrencyConverter();

        var instructions = _extraction.ExtractInstructions(filePath);
        var conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(59033, conversionResult);
        
        instructions.StartCurrency = "AUD";
        instructions.EndCurrency = "JPY";
        instructions.Value = 70;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(6022, conversionResult);

        instructions.StartCurrency = "USD";
        instructions.EndCurrency = "AUD";
        instructions.Value = 615;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(591, conversionResult);

        instructions.StartCurrency = "CHF";
        instructions.EndCurrency = "INR";
        instructions.Value = 359;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(21007, conversionResult);
    }

    [Fact]
    public void TestLongerConversions()
    {
        string filePath = "LongerInstructionsFile.txt";
        IInstructionsExtraction _extraction = new InstructionsExtraction();
        ICurrencyConverter _currencyConverter = new CurrencyConverter();

        var instructions = _extraction.ExtractInstructions(filePath);
        var conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(3202491, conversionResult);
        
        instructions.StartCurrency = "AUD";
        instructions.EndCurrency = "JPY";
        instructions.Value = 70;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(6558, conversionResult);

        instructions.StartCurrency = "CFA";
        instructions.EndCurrency = "JPY";
        instructions.Value = 1000;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(219, conversionResult);
    }

    [Fact]
    public void TestSimplestConversions()
    {
        string filePath = "SimpleInstructionsFile.txt";
        IInstructionsExtraction _extraction = new InstructionsExtraction();
        ICurrencyConverter _currencyConverter = new CurrencyConverter();

        var instructions = _extraction.ExtractInstructions(filePath);

        instructions.StartCurrency = "EUR";
        instructions.EndCurrency = "USD";
        instructions.Value = 1000;
        var conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(1299, conversionResult);

        instructions.StartCurrency = "AUD";
        instructions.EndCurrency = "AUD";
        instructions.Value = 70;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(70, conversionResult);

        instructions.StartCurrency = "AUD";
        instructions.EndCurrency = "USD";
        instructions.Value = 0;
        conversionResult = _currencyConverter.Convert(instructions);
        Assert.Equal(0, conversionResult);
    }

    [Fact]
    public void TestImpossibleConversions()
    {
        string filePath = "SimpleInstructionsFile.txt";
        IInstructionsExtraction _extraction = new InstructionsExtraction();
        ICurrencyConverter _currencyConverter = new CurrencyConverter();

        var instructions = _extraction.ExtractInstructions(filePath);

        instructions.StartCurrency = "EUR";
        instructions.EndCurrency = "CFA";
        instructions.Value = 1000;

        Assert.Throws<CurrencyException>(() => _currencyConverter.Convert(instructions));
    }
}