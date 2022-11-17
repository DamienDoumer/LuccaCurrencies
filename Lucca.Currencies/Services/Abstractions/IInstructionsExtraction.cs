using Lucca.Currencies.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucca.Currencies.Services.Abstractions
{
    public interface IInstructionsExtraction
    {
        Instruction ExtractInstructions(string filePath);
    }
}
