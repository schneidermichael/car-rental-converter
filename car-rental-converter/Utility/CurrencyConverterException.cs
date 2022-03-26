using Grpc.Core;
using GrpcCurrencyConverter;

namespace car_rental_converter.Utility
{
    public class CurrencyConverterException
{
        private CurrencyConverterException()
        {

        }

        public static void CheckSymbol(string symbol)
        {
            SymbolIsNullOrEmpty(symbol);
            SymbolIsTooLong(symbol);
            SymbolIsTooShort(symbol);

        }

        private static void SymbolIsNullOrEmpty(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Parameter `symbol` is empty."));
            }
        }

        private static void SymbolIsTooLong(string symbol)
        {
            if (symbol.Length < 3)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Parameter {symbol} is too short. Only symbols with three letters are allowed. This symbol has {symbol.Length}"));
            }
        }

        private static void SymbolIsTooShort(string symbol)
        {
            if (symbol.Length > 3)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Parameter {symbol} is too long. Only symbols with three letters are allowed. This symbol has {symbol.Length}"));
            }
        }

        public static void CheckAmount(double amount)
        {
            if (amount < 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Parameter {amount} is zero or negativ. Only positive numbers are allowed."));
            }
        }

        public static Currency CheckList(ListOfCurrenciesResponse listOfCurrencies, string symbol)
        {
            Currency currency = new();

            try
            {
                currency = listOfCurrencies.Currencies.Single(currency => currency.Symbol == symbol);

            }
            catch (Exception)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"Parameter {symbol} is not available."));

            }
            return currency;
        }
    }
}
