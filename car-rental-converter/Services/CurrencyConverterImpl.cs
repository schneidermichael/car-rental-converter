using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using grpc_currency_converter.Utility;
using GrpcCurrencyConverter;

namespace grpc_currency_converter.Services
{
    public class CurrencyConverterImpl : CurrencyConverter.CurrencyConverterBase
    {
        public CurrencyConverterImpl()
        {

        }

        public override Task<ListOfCurrenciesResponse> ListOfCurrencies(Empty request, ServerCallContext context)
        {
            return Task.FromResult(CurrencyConverterUtility.GetListOfCurrenciesResponse());
        }

        public override Task<CurrencyPerSymbolResponse> CurrencyPerSymbol(CurrencyPerSymbolRequest request, ServerCallContext context)
        {
            return Task.FromResult(CurrencyConverterUtility.GetCurrencyPerSymbol(request.Symbol));
        }

        public override Task<CalculatingCrossCurrencyResponse> CalculatingCrossCurrency(CalculatingCrossCurrencyRequest request, ServerCallContext context)
        {
            return Task.FromResult(CurrencyConverterUtility.GetCalculatingCrossCurrency(request));
        }
    }
}