namespace Lucca.Currencies.Helpers;

internal class CurrencyException : Exception
{
    public int Code { get; set; }

    public CurrencyException(int code, string message) : base(message)
    {
        
    }
}