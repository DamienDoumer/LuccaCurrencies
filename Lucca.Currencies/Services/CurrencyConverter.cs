using Lucca.Currencies.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucca.Currencies.Algorithms;
using Lucca.Currencies.Helpers;

namespace Lucca.Currencies.Services
{
    /// <summary>
    /// The class in charge of performing currency conversion
    /// </summary>
    public class CurrencyConverter : ICurrencyConverter
    {
        public decimal Convert(Instruction instruction)
        {
            var fastRate = GetRateFast(instruction);
            if (fastRate != default)
                return Math.Round(fastRate * instruction.Value, 0);

            decimal conversionResult = instruction.Value;
            var graph = new Graph(instruction.Vertices, instruction.Nodes);
            var conversionPath = Algorithm.ShortestPathSearch(graph, instruction.StartCurrency, instruction.EndCurrency, instruction.ExchangeRates)
                .ToList();
            if (conversionPath.LastOrDefault() != instruction.EndCurrency)
                throw new CurrencyException(ErrorCodes.ConversionNotPossilbe, "Conversion was not possible");

            for (int index = 0;index < conversionPath.Count - 1;index++)
            {
                var exchangeRate = GetRateFast(instruction, conversionPath[index], conversionPath[index + 1]);

                Debug.WriteLine($"Conversion:{exchangeRate} * {conversionResult}");
                conversionResult = Math.Round(exchangeRate  * conversionResult, 4);
                Debug.WriteLine($"Result = {conversionResult}");
            }

            return Math.Round(conversionResult, 0);
        }

        decimal GetRateFast(Instruction instruction)
        {
            return GetRateFast(instruction, instruction.StartCurrency, instruction.EndCurrency);
        }

        decimal GetRateFast(Instruction instruction, string startCurrency, string endCurrency)
        {
            var key = $"{startCurrency}_{endCurrency}";
            Debug.WriteLine($"Exchange rate retrieved:{key}");

            if (instruction.ExchangeRates.ContainsKey(key))
                return instruction.ExchangeRates[key];

            return default;
        }
    }
}
