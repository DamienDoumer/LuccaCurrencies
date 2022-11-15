using Lucca.Currencies.Algorithms;

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