namespace Lucca.Currencies.Algorithms;


internal class Graph
{
    public Dictionary<string, HashSet<string>> AdjacencyList { get; } = new Dictionary<string, HashSet<string>>();

    public Graph(IEnumerable<string> vertices, IEnumerable<Node> edges)
    {
        foreach (var vertex in vertices)
        {
            AdjacencyList[vertex] = new HashSet<string>();
        }

        foreach (var edge in edges)
        {
            if (AdjacencyList.ContainsKey(edge.StartCurrency) && AdjacencyList.ContainsKey(edge.DestinationCurrency))
            {
                AdjacencyList[edge.StartCurrency].Add(edge.DestinationCurrency);
                AdjacencyList[edge.DestinationCurrency].Add(edge.StartCurrency);
            }
        }
    }
}