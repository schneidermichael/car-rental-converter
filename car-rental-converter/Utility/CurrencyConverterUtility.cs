using GrpcCurrencyConverter;
using System.Xml;

namespace grpc_currency_converter.Utility
{
    public class CurrencyConverterUtility
    {
        private const string EURO_SYMBOL = "EUR";
        private const string XML_TAG_CUBE = "Cube";
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
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == XML_TAG_CUBE))
                {
                    if (!xmlReader.HasAttributes || xmlReader.GetAttribute(XML_ATTRIBUTE_CURRENCY) == null || xmlReader.GetAttribute(XML_ATTRIBUTE_RATE) == null)
                    {
                        continue;
                    }
                    Currency currencyItem = new()
                    {
                        Symbol = xmlReader.GetAttribute(XML_ATTRIBUTE_CURRENCY),
                        Rate = Convert.ToDouble(xmlReader.GetAttribute(XML_ATTRIBUTE_RATE), System.Globalization.CultureInfo.InvariantCulture)
                    };

                    listOfCurrencies.Currencies.Add(currencyItem);
                }
            }
            return listOfCurrencies;
        }

        // Select one specific currency from the list of currencies
        public static CurrencyPerSymbolResponse GetCurrencyPerSymbol(string symbol)
        {

            CurrencyPerSymbolResponse response = new();

            ListOfCurrenciesResponse listOfCurrencies = GetListOfCurrenciesResponse();

            var result = listOfCurrencies.Currencies.Single(currency => currency.Symbol == symbol);

            response.Curreny = result;

            return response;

        }

        //Calculating from currency X to currency Y
        public static CalculatingCrossCurrencyResponse GetCalculatingCrossCurrency(CalculatingCrossCurrencyRequest request)
        {
            CalculatingCrossCurrencyResponse respone = new();

            respone.Symbol = request.SymbolOutput;

            if (request.SymbolInput == EURO_SYMBOL)
            {
                CurrencyPerSymbolResponse output = GetCurrencyPerSymbol(request.SymbolOutput);

                respone.Result = output.Curreny.Rate * request.Amount;

                return respone;

            }
            else if (request.SymbolOutput == EURO_SYMBOL)
            {
                CurrencyPerSymbolResponse input = GetCurrencyPerSymbol(request.SymbolInput);

                respone.Result = (1 / input.Curreny.Rate) * request.Amount;

                return respone;

            }

            CurrencyPerSymbolResponse currencyInput = GetCurrencyPerSymbol(request.SymbolInput);

            CurrencyPerSymbolResponse currencyOutput = GetCurrencyPerSymbol(request.SymbolOutput);

            respone.Result = (currencyOutput.Curreny.Rate / currencyInput.Curreny.Rate) * request.Amount;

            return respone;
        }
    }
}
