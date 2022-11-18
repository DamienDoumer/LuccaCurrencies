using Lucca.Currencies.Helpers;

namespace Lucca.Currencies.Algorithms;

internal static class Algorithm
{
    public static IEnumerable<string> ShortestPathSearch(Graph graph, string startCurrency, string endCurrency, Dictionary<string, decimal> exchangeRates)
    {
        var visited = new HashSet<string>();
        var conversionPath = new HashSet<string>();
        var previousCurrency = startCurrency;

        if (!graph.AdjacencyList.ContainsKey(startCurrency) || !graph.AdjacencyList.ContainsKey(endCurrency))
            throw new CurrencyException(ErrorCodes.CurrencyNotHandled,
                "The destination or start currency are not handled");

        var queue = new Queue<string>();
        queue.Enqueue(startCurrency);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();

            if (visited.Contains(vertex))
                continue;

            visited.Add(vertex);
            
            foreach (var neighbor in graph.AdjacencyList[vertex])
            {
                if (endCurrency?.Equals(vertex) ?? false)
                {
                    conversionPath.Add(vertex);
                    break;
                }

                if (!visited.Contains(neighbor))
                {
                    queue.Enqueue(neighbor);
                    if (previousCurrency == vertex || exchangeRates.ContainsKey($"{previousCurrency}_{vertex}"))
                        conversionPath.Add(vertex);
                    previousCurrency = vertex;
                }
            }
        }

        return conversionPath;
    }
}