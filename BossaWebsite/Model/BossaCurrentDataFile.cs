using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BossaWebsite.Model
{
    public abstract class BossaCurrentDataFile<RecordType> : BossaDataFile
    {
        private List<RecordType> _dataList;

        protected override void Initialize(Stream stream)
        {
            var cultureInfo = CultureInfo.InvariantCulture;

            using var reader = new StreamReader(stream, Encoding.ASCII);
            using var csv = new CsvReader(reader, cultureInfo);

            csv.Configuration.HasHeaderRecord = false;
            _dataList = csv.GetRecords<RecordType>().ToList();
        }

        public IQueryable<RecordType> Data => _dataList.AsQueryable();
    }
}
