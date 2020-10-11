using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.Parsers.PocztowyXmlOperationsFile.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Parsers.PocztowyXmlOperationsFile
{
    class PocztowyXmlOperationsFileParser : XmlOperationsFileParser<PocztowyXmlOperationsFileContract>
    {
        public override Encoding FileEncoding => Encoding.UTF8;

        public override List<Operation> MapToOperationsFile(string xml)
        {
            var fileData = ParseXml(xml);

            var r = from f in fileData.Operations
                    select new Operation()
                    {
                        AccountCode = fileData.Account,
                        Amount = f.Amount,
                        Description = f.Type + " " + GetDescription(f),
                        Currency = f.CurrencySymbol,
                        Date = new DateTime[] { f.TransactionDate, f.OrderDate, f.ProcessingDate }.Min()
                    };

            return r.ToList();
        }

        private static string GetDescription(PocztowyXmlOperationsFileOperationContract operation)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Rachunek przeciwstawny: ");
            sb.AppendLine(operation.OppositeAccount);

            sb.Append("Dane kontrahenta: ");
            sb.AppendLine(string.Join(" ", operation.OppositeLines));

            sb.Append("Tytuł: ");
            sb.AppendLine(string.Join(" ", operation.DetailsLines));

            return sb.ToString();
        }
    }
}
