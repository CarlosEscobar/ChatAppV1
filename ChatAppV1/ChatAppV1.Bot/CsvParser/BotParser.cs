using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace ChatAppV1.Bot.CsvParser
{
    public class BotParser
    {
        private readonly Stream csvStream;

        public BotParser(Stream csvStream)
        {
            this.csvStream = csvStream;
        }

        public StockModel Parse()
        {
            using (CsvReader csvReader = new CsvReader(new StreamReader(csvStream)))
            {
                csvReader.Configuration.RegisterClassMap<StockMapper>();
                using (IEnumerator<StockModel> iterator = csvReader.GetRecords<StockModel>().GetEnumerator())
                {
                    iterator.MoveNext();
                    return iterator.Current;
                }
            }
        }
    }
}
