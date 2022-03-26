using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using grpc_currency_converter.Utility;
using GrpcCurrencyConverter;
using Microsoft.AspNetCore.Authorization;

namespace grpc_currency_converter.Services
{
    public class CurrencyConverterImpl : CurrencyConverter.CurrencyConverterBase
    {
        public CurrencyConverterImpl()
        {
  
        }
        [Authorize]
        public override Task<ListOfCurrenciesResponse> ListOfCurrencies(Empty request, ServerCallContext context)
        {
            return Task.FromResult(CurrencyConverterUtility.GetListOfCurrenciesResponse());
        }

        [Authorize]
        public override Task<CurrencyPerSymbolResponse> CurrencyPerSymbol(CurrencyPerSymbolRequest request, ServerCallContext context)
        {
            return Task.FromResult(CurrencyConverterUtility.GetCurrencyPerSymbol(request.Symbol));
        }

        [Authorize]
        public override Task<CalculatingCrossCurrencyResponse> CalculatingCrossCurrency(CalculatingCrossCurrencyRequest request, ServerCallContext context)
        {
            return Task.FromResult(CurrencyConverterUtility.GetCalculatingCrossCurrency(request));
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context) => Task.FromResult(CurrencyConverterUtility.GetLogin(request));
    }
}