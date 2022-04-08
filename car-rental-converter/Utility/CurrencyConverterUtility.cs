using car_rental_converter.Utility;
using Grpc.Core;
using GrpcCurrencyConverter;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml;

namespace grpc_currency_converter.Utility
{
    public class CurrencyConverterUtility
    {
        private const string EURO_SYMBOL = "EUR";
        private const string XML_ATTRIBUTE_CURRENCY = "currency";
        private const string XML_ATTRIBUTE_RATE = "rate";
        private const string HTTPS = "https://";
        private const string URL = "www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
        // TODO: Hardcoded Username + Password not perfect -> better use appsettings.json
        private const string USERNAME = "group1";
        private const string PASSWORD = "car";

        private static readonly JwtSecurityTokenHandler JwtTokenHandler = new();
        public static readonly SymmetricSecurityKey SecurityKey = new(Guid.NewGuid().ToByteArray());
        
        protected CurrencyConverterUtility()
        {
       
        }

        // Load the List of available Currencies from the European Central Bank
        public static ListOfCurrenciesResponse GetListOfCurrenciesResponse()
        {
            ListOfCurrenciesResponse listOfCurrencies = new();

            XmlReader xmlReader = XmlReader.Create(HTTPS + URL);

            while (xmlReader.Read())
            {
                if (xmlReader.GetAttribute(XML_ATTRIBUTE_CURRENCY) != null)
                {

                    Currency currencyItem = new()
                    {
                        Symbol = xmlReader.GetAttribute(XML_ATTRIBUTE_CURRENCY),
                        Rate = Convert.ToDouble(xmlReader.GetAttribute(XML_ATTRIBUTE_RATE), System.Globalization.CultureInfo.InvariantCulture)
                    };

                    listOfCurrencies.Currencies.Add(currencyItem);
                }
            }

            listOfCurrencies.Currencies.Add(Euro());

            return listOfCurrencies;
        }

        // Select one specific currency from the list of currencies
        public static CurrencyPerSymbolResponse GetCurrencyPerSymbol(string symbol)
        {
            CurrencyConverterException.CheckSymbol(symbol);

            CurrencyPerSymbolResponse response = new();

            ListOfCurrenciesResponse listOfCurrencies = GetListOfCurrenciesResponse();

            response.Curreny = CurrencyConverterException.CheckList(listOfCurrencies, symbol);

            return response;
        }

        //Calculating from currency X to currency Y
        public static CalculatingCrossCurrencyResponse GetCalculatingCrossCurrency(CalculatingCrossCurrencyRequest request)
        {
            CurrencyConverterException.CheckAmount(request.Amount);

            CalculatingCrossCurrencyResponse respone = new();

            ListOfCurrenciesResponse listOfCurrencies = GetListOfCurrenciesResponse();

            Currency currencyInput = listOfCurrencies.Currencies.Single(currency => currency.Symbol == request.SymbolInput);
            Currency currencyOutput = listOfCurrencies.Currencies.Single(currency => currency.Symbol == request.SymbolOutput);

            respone.Symbol = request.SymbolOutput;

            if (request.SymbolInput == EURO_SYMBOL)
            {
                respone.Result = currencyOutput.Rate * request.Amount;
            }
            else if (request.SymbolOutput == EURO_SYMBOL)
            {
                respone.Result = (1 / currencyInput.Rate) * request.Amount;
            }
            else
            {
                respone.Result = (currencyOutput.Rate / currencyInput.Rate) * request.Amount;
            }

            return respone;
        }

        public static LoginResponse GetLogin(LoginRequest request)
        {
            LoginResponse respone = new();

            if(request.Username == USERNAME && request.Password == PASSWORD)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, USERNAME) };
                var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken("car-rental-converter", "car-rental-app", claims, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
                respone.Token = JwtTokenHandler.WriteToken(token);
                return respone;
            }

            throw new RpcException(new Status(StatusCode.InvalidArgument, "Username or Password is incorrect"));
        }

        private static Currency Euro()
        {
            Currency euro = new();
            euro.Symbol = "EUR";
            euro.Rate = 1.00;

            return euro;
        }

    }
}
