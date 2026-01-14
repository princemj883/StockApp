using System.Text.Json;
using StocksApp.ServiceContracts;

namespace StocksApp.Services;

public class FinnhubService : IFinnhubService
{
    private readonly IHttpClientFactory _clientFactory;

    public FinnhubService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        using HttpClient httpClient = _clientFactory.CreateClient();
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
        {
            RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token=d5j1sfpr01qicq2lhudgd5j1sfpr01qicq2lhue0"),
            Method = HttpMethod.Get
        };
        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        Stream stream = httpResponseMessage.Content.ReadAsStream();
        
        StreamReader reader = new StreamReader(stream);
        string response = reader.ReadToEnd();
        Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
        
        if(responseDictionary == null)
            throw new InvalidOperationException("No response from Finnhub API");
        
        if(responseDictionary.ContainsKey("error"))
            throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
        return responseDictionary;
    }
}