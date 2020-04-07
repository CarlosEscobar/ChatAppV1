using ChatAppV1.Bot.CsvParser;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatAppV1.Bot
{
    public class ChatAppBot
    {
        private readonly HttpClient client;
        private readonly string stockCode;

        public ChatAppBot(HttpClient client, string stockCode)
        {
            this.client = client;
            this.stockCode = stockCode;
        }

        private string stooqApiUrl
        {
            get
            {
                return "https://stooq.com/q/l/?s=" + stockCode + "&f=sd2t2ohlcv&h&e=csv";
            }
        }
        
        public async Task<string> ProcessStockCode()
        {
            try
            {
                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, stooqApiUrl));
                if (response.IsSuccessStatusCode)
                {
                    StockModel stockModel = new BotParser(await response.Content.ReadAsStreamAsync()).Parse();
                    if (stockModel.Close == "N/D")
                    {
                        return "DATA NOT FOUND FOR THE STOCK CODE: " + stockCode.ToUpper() + ".";
                    }
                    return stockCode.ToUpper() + " quote is $" + stockModel.Close + " per share.";
                }
                else
                {
                    return "THE STOCK SERVER RETURNED AN ERROR.";
                }
            }
            catch
            {
                return "THERE WAS AN ERROR PROCESSING THE STOCK CODE: " + stockCode.ToUpper() + ".";
            }
        }
    }
}
