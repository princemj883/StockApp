namespace StocksApp.Services;

public class MyService
{
    private readonly IHttpClientFactory _clientFactory;

    public MyService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<string> Method()
    {
        using HttpClient httpClient = _clientFactory.CreateClient();
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=AAPL&token=d5j1sfpr01qicq2lhudgd5j1sfpr01qicq2lhue0"),
            Method = HttpMethod.Get
        };
        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        string response = await httpResponseMessage.Content.ReadAsStringAsync();
        return response;
    }
}