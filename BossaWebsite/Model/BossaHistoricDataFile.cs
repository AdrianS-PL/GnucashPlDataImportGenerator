using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace BossaWebsite.Model
{
    public abstract class BossaHistoricDataFile<RecordType> : BossaDataFile
    {
        const string DataFileExtension = ".mst";
        protected override void Initialize(Stream stream)
        {
            using var zipArchive = new ZipArchive(stream);

            var dataFilesEntries = zipArchive.Entries.Where(q => q.FullName == q.Name && q.Name.EndsWith(DataFileExtension));

            foreach(var dataFilesZipEntry in dataFilesEntries)
            {
                using var dataFileStream = dataFilesZipEntry.Open();

                var cultureInfo = CultureInfo.InvariantCulture;

                using var reader = new StreamReader(dataFileStream, Encoding.ASCII);
                using var csv = new CsvReader(reader, cultureInfo);

                csv.Configuration.HasHeaderRecord = true;
                Data.Add(dataFilesZipEntry.Name.Replace(DataFileExtension, string.Empty), csv.GetRecords<RecordType>().ToList());
            }
        }

        public Dictionary<string, List<RecordType>> Data { get; } = new Dictionary<string, List<RecordType>>();
    }
}
