using Bot.Clients;
using Bot.Exceptions;
using Bot.Models;
using Bot.Services;
using FluentAssertions;
using Moq;

namespace Bot.UnitTests.Services;

public class StockServiceTest
{
    private readonly Mock<IStockRestClient> _stockRestClientMock = new();
    private StockService? _sut;


    [Fact]
    public async void ShouldGetStocksWithValidStockCode()
    {
        _sut = new StockService(_stockRestClientMock.Object);
        var stockCode = "AAPL.US";
        _stockRestClientMock.Setup(client => client.GetStocksAsync(It.IsAny<string>()))
            .ReturnsAsync(() => new List<Stock>
            {
                new()
                {
                    Symbol = "AAPL.US",
                    Date = "2022-09-02",
                    Time = "22:00:07",
                    Open = "159.75",
                    High = "160.362",
                    Low = "154.965",
                    Close = "155.81",
                    Volume = "76957768"
                }
            });
        var stocks = await _sut.GetStockByCodeAsync(stockCode);
        stocks.First().Symbol.Should().Be(stockCode);
        stocks.Should().NotBeEmpty();
    }

    [Fact]
    public void ShouldThrowIfStockCodeIsInvalid()
    {
        _sut = new StockService(_stockRestClientMock.Object);
        var stockCode = "Unknown";
        _stockRestClientMock.Setup(client => client.GetStocksAsync(stockCode))
            .ReturnsAsync(() => throw new CustomException("something wrong"));

        _sut.Invoking(service =>
            service.GetStockByCodeAsync(It.IsAny<string>())
                .Should()
        );
        Action act = () => _sut.GetStockByCodeAsync(stockCode);
        act.Should().Throw<CustomException>();
    }
}