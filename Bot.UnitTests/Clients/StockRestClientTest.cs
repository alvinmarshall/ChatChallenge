using System.Net;
using Bot.Clients;
using Bot.Options;
using FluentAssertions;
using Moq;
using Moq.Protected;

namespace Bot.UnitTests.Clients;

public class StockRestClientTest
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock = new();
    private StockRestClient? _sut;

    [Fact]
    public async void ShouldFetchStockAsList()
    {
        const string body = @"Symbol,Date,Time,Open,High,Low,Close,Volume
                      AAPL.US,2022-09-02,22:00:07,159.75,160.362,154.965,155.81,76957768";
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(
                new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(body) }
            );

        var client = new HttpClient(_httpMessageHandlerMock.Object);
        _sut = new StockRestClient(client, new StockConfigOption { BaseUrl = "localhost:9000" });
        var stockName = "AAPL.US";
        var stocks = await _sut.GetStocksAsync(stockName);
        stocks.Should().NotBeEmpty();
    }
}