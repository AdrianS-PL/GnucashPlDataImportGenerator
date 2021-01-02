using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace StooqWebsite.Model
{
    public class StooqDataFile
    {
        private List<StooqDataFileRow> _dataList;

        public StooqDataFile(Stream stream)
        {
            var cultureInfo = CultureInfo.InvariantCulture;

            using var reader = new StreamReader(stream, Encoding.ASCII);
            using var csv = new CsvReader(reader, cultureInfo);            

            csv.Configuration.HasHeaderRecord = true;
            _dataList = csv.GetRecords<StooqDataFileRow>().ToList();
        }

        public IQueryable<StooqDataFileRow> Data => _dataList.AsQueryable();
    }
}
