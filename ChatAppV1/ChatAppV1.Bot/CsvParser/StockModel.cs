using CsvHelper.Configuration.Attributes;

namespace ChatAppV1.Bot.CsvParser
{
    public class StockModel
    {
        [Name("Symbol")]
        public string Symbol { get; set; }

        [Name("Date")]
        public string Date { get; set; }

        [Name("Time")]
        public string Time { get; set; }

        [Name("Open")]
        public string Open { get; set; }

        [Name("High")]
        public string High { get; set; }

        [Name("Low")]
        public string Low { get; set; }

        [Name("Close")]
        public string Close { get; set; }

        [Name("Volume")]
        public string Volume { get; set; }
    }
}
