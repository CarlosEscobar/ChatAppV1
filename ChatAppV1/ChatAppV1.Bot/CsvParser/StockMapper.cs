using CsvHelper.Configuration;

namespace ChatAppV1.Bot.CsvParser
{
    public sealed class StockMapper : ClassMap<StockModel>
    {
        public StockMapper()
        {
            Map(m => m.Symbol).Name("Symbol");
            Map(m => m.Date).Name("Date");
            Map(m => m.Time).Name("Time");
            Map(m => m.Open).Name("Open");
            Map(m => m.High).Name("High");
            Map(m => m.Low).Name("Low");
            Map(m => m.Close).Name("Close");
            Map(m => m.Volume).Name("Volume");
        }
    }
}
