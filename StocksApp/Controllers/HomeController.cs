using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;

namespace StocksApp.Controllers;

public class HomeController(FinnhubService finnhubService, IOptions<TradingOptions> tradingOptions)
    : Controller
{
    // GET
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        if (tradingOptions.Value.DefaultStockSymbol == null)
        {
            tradingOptions.Value.DefaultStockSymbol = "MSFT";
        }
        Dictionary<string, object>? responseDictionary = await finnhubService.GetStockPriceQuote(tradingOptions.Value.DefaultStockSymbol);

        Stocks stock = new Stocks()
        {
            StockSymbol = tradingOptions.Value.DefaultStockSymbol,
            CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()),
            HighPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
            LowPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
            OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString()),
            PreviousClosePrice = Convert.ToDouble(responseDictionary["pc"].ToString())
            
        };
        return Ok(stock);
    }
}