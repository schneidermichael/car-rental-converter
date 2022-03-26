using car_rental_converter.Utility;
using Grpc.Core;
using GrpcCurrencyConverter;
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

        private CurrencyConverterUtility()
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

            CurrencyPerSymbolResponse currencyInput = GetCurrencyPerSymbol(request.SymbolInput);
            CurrencyPerSymbolResponse currencyOutput = GetCurrencyPerSymbol(request.SymbolOutput);

            respone.Symbol = request.SymbolOutput;

            if (request.SymbolInput == EURO_SYMBOL)
            {
                respone.Result = currencyOutput.Curreny.Rate * request.Amount;
            }
            else if (request.SymbolOutput == EURO_SYMBOL)
            {
                respone.Result = (1 / currencyInput.Curreny.Rate) * request.Amount;
            }
            else
            {
                respone.Result = (currencyOutput.Curreny.Rate / currencyInput.Curreny.Rate) * request.Amount;
            }

            return respone;
        }

        private static Currency Euro()
        {
            Currency euro = new();
            euro.Symbol = "EUR";

            return euro;
        }

    }
}
