using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucca.Currencies.Helpers;

namespace Lucca.Currencies.Services.Abstractions
{
    public interface ICurrencyConverter
    {
        decimal Convert(Instruction instruction);
    }
}
