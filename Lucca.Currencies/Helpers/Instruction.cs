using Lucca.Currencies.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucca.Currencies.Helpers
{
    internal class Instruction
    {
        public string StartCurrency { get; set; }
        public string EndCurrency { get; set; }
        public decimal Value { get; set; }
        public int Iterations { get; set; }
        public List<Node> Nodes { get; set; }
        public HashSet<string> Vertices { get; private set; }
        public Dictionary<string, decimal> ExchangeRates { get; set; }

        public Instruction()
        {
            StartCurrency = string.Empty;
            EndCurrency = string.Empty;
            ExchangeRates = new Dictionary<string, decimal>();
            Nodes = new List<Node>();
            Vertices = new HashSet<string>();
        }
    }
}
