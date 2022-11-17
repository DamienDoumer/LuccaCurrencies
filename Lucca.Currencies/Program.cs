using Lucca.Currencies;
using Lucca.Currencies.Algorithms;
using Lucca.Currencies.Helpers;
using Lucca.Currencies.Services;
using Lucca.Currencies.Services.Abstractions;

var filePath = args[0];
IInstructionsExtraction _extraction;
ICurrencyConverter _currencyConverter;

try
{
    _extraction = new InstructionsExtraction();
    _currencyConverter = new CurrencyConverter();

    var instructions = _extraction.ExtractInstructions(filePath);
    var conversionResult = _currencyConverter.Convert(instructions);

    Console.WriteLine(conversionResult);
}
catch (CurrencyException e) when (e.Code == ErrorCodes.CurrencyNotHandled)
{
    Console.WriteLine(Texts.CurrencyNotHandledErrorMessage);
}
catch (CurrencyException e) when (e.Code == ErrorCodes.ConversionNotPossilbe)
{
    Console.WriteLine(Texts.ConversionNotPossilbeErrorMessage);
}
catch (CurrencyException e) when (e.Code == ErrorCodes.BadInstructionsFound)
{
    Console.WriteLine(e.Message);
}
catch (CurrencyException e) when (e.Code == ErrorCodes.InsufficientInstructionsFound)
{
    Console.WriteLine(Texts.InsufficientInstructionsErrorCode);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}



var vertices = new[] { "AUD", "CHF", "JPY", "KWU", "EUR", "USD", "INR", "Shit" };
var edges = new[]
{
    new Node("AUD", "CHF"), new Node("JPY", "KWU"),
    new Node("EUR", "CHF"), new Node("AUD", "JPY"),
    new Node("EUR", "USD"), new Node("JPY", "INR"),
};

var graph = new Graph(vertices, edges);

Console.WriteLine($"Final: {string.Join(", ", Algorithm.ShortestPathSearch(graph, "USD", "AUD"))}");
Console.ReadKey();