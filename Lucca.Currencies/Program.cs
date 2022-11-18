using Lucca.Currencies;
using Lucca.Currencies.Algorithms;
using Lucca.Currencies.Helpers;
using Lucca.Currencies.Services;
using Lucca.Currencies.Services.Abstractions;

//var filePath = args[0];
//IInstructionsExtraction _extraction;
//ICurrencyConverter _currencyConverter;

//try
//{
//    _extraction = new InstructionsExtraction();
//    _currencyConverter = new CurrencyConverter();

//    var instructions = _extraction.ExtractInstructions(filePath);
//    var conversionResult = _currencyConverter.Convert(instructions);

//    Console.WriteLine(conversionResult);
//}
//catch (CurrencyException e) when (e.Code == ErrorCodes.CurrencyNotHandled)
//{
//    Console.WriteLine(Texts.CurrencyNotHandledErrorMessage);
//}
//catch (CurrencyException e) when (e.Code == ErrorCodes.ConversionNotPossilbe)
//{
//    Console.WriteLine(Texts.ConversionNotPossilbeErrorMessage);
//}
//catch (CurrencyException e) when (e.Code == ErrorCodes.BadInstructionsFound)
//{
//    Console.WriteLine(e.Message);
//}
//catch (CurrencyException e) when (e.Code == ErrorCodes.InsufficientInstructionsFound)
//{
//    Console.WriteLine(Texts.InsufficientInstructionsErrorCode);
//}
//catch (Exception e)
//{
//    Console.WriteLine(e);
//    throw;
//}



var vertices = new[] { "AUD", "CHF", "JPY", "KWU", "EUR", "USD", "INR", "Shit" };
var exchangeRates = new Dictionary<string, decimal>()
{
    { "AUD_CHF", 0},
    { "CHF_AUD", 0},
    { "JPY_KWU", 0},
    { "KWU_JPY", 0},
    { "EUR_CHF", 0},
    { "CHF_EUR", 0},
    { "AUD_JPY", 0},
    { "JPY_AUD", 0},
    { "EUR_USD", 0},
    { "USD_EUR", 0},
    { "JPY_INR", 0},
    { "INR_JPY", 0},
};
var edges = new[]
{
    new Node("AUD", "CHF"), new Node("JPY", "KWU"),
    new Node("EUR", "CHF"), new Node("AUD", "JPY"),
    new Node("EUR", "USD"), new Node("JPY", "INR"),
};

var graph = new Graph(vertices, edges);

Console.WriteLine($"Final: {string.Join(", ", Algorithm.ShortestPathSearch(graph, "CHF", "INR", exchangeRates))}");
Console.ReadKey();