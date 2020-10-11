using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlOperationsFile.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlOperationsFile
{
    class PkoBpXmlOperationsFileParser : XmlOperationsFileParser<PkoBpAccountOperationsXmlFile>
    {
        public override Encoding FileEncoding => Encoding.UTF8;

        public override List<Operation> MapToOperationsFile(string xml)
        {
            var fileData = ParseXml(xml);

            var r = from f in fileData.Operations
                    select new Operation()
                    {
                        AccountCode = fileData.SearchParameters.Account,
                        Amount = f.Amount.Value,
                        Description = f.Type + " " + f.Description,
                        Currency = f.Amount.CurrencySymbol,
                        Date = new DateTime[] { f.ExecutionDate, f.OrderDate }.Min()
                    };

            return r.ToList();
        }
    }
}
