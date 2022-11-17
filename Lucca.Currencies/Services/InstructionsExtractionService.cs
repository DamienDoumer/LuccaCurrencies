﻿using System.Text.RegularExpressions;
using Lucca.Currencies;
using Lucca.Currencies.Algorithms;
using Lucca.Currencies.Helpers;
using Lucca.Currencies.Services.Abstractions;

/// <summary>
/// Reads and extracts instructions from the given file.
/// </summary>
internal class InstructionsExtractionService : IInstructionsExtractionService
{
    private readonly string _filePath;
    private const char InstructionDelimiter = ';';

    public InstructionsExtractionService(string filePath)
    {
        _filePath = filePath;
        if (!File.Exists(filePath))
            throw new FileNotFoundException();
    }

    private Queue<string> ReadFileContent()
    {
        var lines = new Queue<string>();
        var line = string.Empty;
        using var reader = new StreamReader(_filePath);

        while ((line = reader.ReadLine()) != null)
        {
            lines.Enqueue(line);
        }

        if (!lines.Any() || lines.Count < 3)
            throw new CurrencyException(ErrorCodes.InsufficientInstructionsFound, "Not enough instructions");

        return lines;
    }

    private bool IsConversionInstructionValid(string[] conversionInstructionParts)
    {
        if (conversionInstructionParts.Length != 3)
            return false;

        if (conversionInstructionParts[0].Length != 3 && conversionInstructionParts[2].Length != 3)
            return false;

        if (!Regex.IsMatch(conversionInstructionParts[0], @"^[a-zA-Z]+$")
            || !Regex.IsMatch(conversionInstructionParts[2], @"^[a-zA-Z]+$"))
            return false;

        return true;
    }

    private bool IsConversionRateValid(string[] conversionRateParts)
    {
        if (conversionRateParts.Length != 3)
            return false;

        if (conversionRateParts[0].Length != 3 && conversionRateParts[1].Length != 3)
            return false;

        if (!Regex.IsMatch(conversionRateParts[0], @"^[a-zA-Z]+$")
            || !Regex.IsMatch(conversionRateParts[1], @"^[a-zA-Z]+$"))
            return false;

        return true;
    }

    public Instruction ExtractInstructions()
    {
        var instructionLines = ReadFileContent();
        return ExtractInstructions(instructionLines);
    }

    public Instruction ExtractInstructions(Queue<string> instructionLines)
    {
        var instruction = new Instruction();
        int currentInstructionLine = 3;
        var conversionInstruction = instructionLines.Dequeue();
        var currencyNodeCount = instructionLines.Dequeue();

        var conversionInstructionParts = conversionInstruction.Split(InstructionDelimiter);
        if (!IsConversionInstructionValid(conversionInstructionParts))
            throw new CurrencyException(ErrorCodes.BadInstructionsFound,
                string.Format(Texts.BadInstructions, 1));

        instruction.StartCurrency = conversionInstructionParts[0].ToUpper();
        instruction.EndCurrency = conversionInstructionParts[0].ToUpper();

        bool isNumber = int.TryParse(conversionInstructionParts[1], out int conversionValue);
        bool isLine2ANumber = int.TryParse(currencyNodeCount, out int iterations);
        if (!isNumber)
            throw new CurrencyException(ErrorCodes.BadInstructionsFound,
                string.Format(Texts.BadInstructions, 1));
        if (!isLine2ANumber)
            throw new CurrencyException(ErrorCodes.BadInstructionsFound,
                string.Format(Texts.BadInstructions, 2));

        instruction.Value = conversionValue;
        instruction.Iterations = iterations;

        for (int i = 0; i < iterations; i++)
        {
            var latestInstruction = instructionLines.Dequeue();
            var latestInstructionPart = latestInstruction.Split(InstructionDelimiter);

            if (!IsConversionRateValid(latestInstructionPart))
                throw new CurrencyException(ErrorCodes.BadInstructionsFound,
                    string.Format(Texts.BadInstructions, currentInstructionLine));

            var isValidNumber = decimal.TryParse(latestInstructionPart[2], out decimal exchangeRate);
            if (!isValidNumber)
                throw new CurrencyException(ErrorCodes.BadInstructionsFound,
                    string.Format(Texts.BadInstructions, currentInstructionLine));

            var startCurrency = latestInstructionPart[0].ToUpper();
            var endCurrency = latestInstructionPart[1].ToUpper();
            instruction.Nodes.Add(new Node(startCurrency, endCurrency));
            instruction.Vertices.Add(startCurrency);
            instruction.Vertices.Add(endCurrency);

            if (!instruction.ExchangeRates.ContainsKey($"{startCurrency}_{endCurrency}"))
                instruction.ExchangeRates.Add($"{startCurrency}_{endCurrency}", exchangeRate);
            if (!instruction.ExchangeRates.ContainsKey($"{endCurrency}_{startCurrency}"))
                instruction.ExchangeRates.Add($"{endCurrency}_{startCurrency}", Math.Round(1 / exchangeRate, 4));

            currentInstructionLine++;
        }

        return instruction;
    }
}