using Lucca.Currencies.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucca.Currencies.Services.Abstractions
{
    internal interface IInstructionsExtractionService
    {
        Instruction ExtractInstructions(Queue<string> instructionLines);
        Instruction ExtractInstructions();
    }
}
