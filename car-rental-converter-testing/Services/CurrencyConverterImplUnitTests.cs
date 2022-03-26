using Google.Protobuf.WellKnownTypes;
using grpc_currency_converter.Services;
using grpc_currency_converter.UnitTests;
using GrpcCurrencyConverter;
using System.Threading.Tasks;
using Xunit;

namespace car_rental_converter_testing
{
    public class CurrencyConverterImplUnitTests
    {

        [Fact]
        public async Task ListOfCurrenciesTest()
        {
            //Arrange
            var service = new CurrencyConverterImpl();
            //Act

            var response = await service.ListOfCurrencies(new Empty(), TestServerCallContext.Create());

            //Assert
            Assert.Equal(32, response.Currencies.Count);
        }

        [Fact]
        public async Task CurrencyPerSymbolTest()
        {
            // Arrange
            // var mockObject = new Mock<CurrencyConverterImpl>();
            // mockObject.Setup(m => m.CurrencyPerSymbol(It.IsAny<CurrencyPerSymbolRequest>(), It.IsAny<ServerCallContext>())).Returns((CurrencyPerSymbolResponse r, ServerCallContext context) => Task.FromResult(r));
            var service = new CurrencyConverterImpl();

            // Act
            var response = await service.CurrencyPerSymbol(new CurrencyPerSymbolRequest { Symbol = "USD" }, TestServerCallContext.Create());

            // Assert
            Assert.Equal("USD", response.Curreny.Symbol);
        }

        [Fact]
        public async Task CalculatingCrossCurrencyTest()
        {
            //Arrange
            var service = new CurrencyConverterImpl();

            //Act
            var response = await service.CalculatingCrossCurrency(new CalculatingCrossCurrencyRequest
            {
                Amount = 1.00,
                SymbolInput = "EUR",
                SymbolOutput = "JPY"
            }, TestServerCallContext.Create());

            //Assert
            Assert.Equal("JPY", response.Symbol);

        }
        [Fact]
        public async Task LoginTest()
        {
            //Arrange
            var service = new CurrencyConverterImpl();

            //act
            var response = await service.Login(new LoginRequest
            {
                Username = "group1",
                Password = "car"

            }, TestServerCallContext.Create());

            //Assert
            Assert.NotNull(response.Token);
        }
    }
}