using CsvHelper.Configuration.Attributes;
using System;

namespace StooqWebsite.Model
{
    public class StooqDataFileRow
    {
        [Index(0)]
        [Format("yyyy-MM-dd")]
        public DateTime Date { get; set; }

        [Index(1)]
        public decimal OpeningPrice { get; set; }

        [Index(2)]
        public decimal MaxPrice { get; set; }

        [Index(3)]
        public decimal MinPrice { get; set; }

        [Index(4)]
        public decimal ClosingPrice { get; set; }
    }
}
