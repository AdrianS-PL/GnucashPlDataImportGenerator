using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    internal class PolishTreasuryBondsAccountStateXlsFileParser : IPolishTreasuryBondsAccountStateFileParser
    {
        public const string Extension = "xls";

        public bool IsExtensionProcessable(string extension)
        {
            if (extension[0] == '.')
                extension = extension.Substring(1);

            return extension.ToLower() == Extension;
        }

        public async Task<IList<PolishTreasuryBondsAccountStateFileRecord>> ParseFile(string filename)
        {
            var result = new List<PolishTreasuryBondsAccountStateFileRecord>();

            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);
                do
                {
                    reader.Read();
                    while (reader.Read())
                    {
                        var record = new PolishTreasuryBondsAccountStateFileRecord()
                        {
                            EmissionCode = reader.GetString(0),
                            AvailableBonds = (int)Math.Round(reader.GetDouble(1)),
                            BlockedBonds = (int)Math.Round(reader.GetDouble(2)),
                            NominalValue = new decimal(reader.GetDouble(3)),
                            CurrentValue = new decimal(reader.GetDouble(4)),
                            BuyoutDate = DateTime.Parse(reader.GetString(5), CultureInfo.InvariantCulture)
                        };

                        result.Add(record);
                    }
                } while (reader.NextResult());
            }
            
            await Task.CompletedTask;
            return result;
        }
    }
}
