using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GnuCash.TransactionImportGenerator.Parsers.PocztowyXmlOperationsFile.DataContract
{
    [ExcludeFromCodeCoverage]
    [XmlRoot(ElementName = "acc_history", IsNullable = false, Namespace = @"http://api.bpsa.pl/historiaTransakcji")]
    public class PocztowyXmlOperationsFileContract
    {
        [XmlElement(ElementName = "acc", IsNullable = false)]
        public string Account { get; set; }

        [XmlElement(ElementName = "acc-curr", IsNullable = false)]
        public string AccountCurrencySymbol { get; set; }

        [XmlElement(ElementName = "date-from", IsNullable = false)]
        public DateTime DateFrom { get; set; }

        [XmlElement(ElementName = "date-to", IsNullable = false)]
        public DateTime DateTo { get; set; }

        [XmlElement(ElementName = "opening-balance", IsNullable = false)]
        public decimal OpeningBalance { get; set; }

        [XmlElement(ElementName = "closing-balance", IsNullable = false)]
        public decimal ClosingBalance { get; set; }

        [XmlElement(ElementName = "operation")]
        public List<PocztowyXmlOperationsFileOperationContract> Operations { get; set; }
    }
}
