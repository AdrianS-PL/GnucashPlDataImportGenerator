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
    public class PocztowyXmlOperationsFileOperationContract
    {        
        [XmlElement(ElementName = "document-nr", IsNullable = false)]
        public int DocumentNr { get; set; }

        [XmlElement(ElementName = "decree-nr", IsNullable = false)]
        public int DecreeNr { get; set; }

        [XmlElement(ElementName = "transaction-date", IsNullable = false)]
        public DateTime TransactionDate { get; set; }

        [XmlElement(ElementName = "order-date", IsNullable = false)]
        public DateTime OrderDate { get; set; }

        [XmlElement(ElementName = "proc-date", IsNullable = false)]
        public DateTime ProcessingDate { get; set; }

        [XmlElement(ElementName = "acc", IsNullable = false)]
        public string Account { get; set; }

        [XmlElement(ElementName = "opposite-acc", IsNullable = false)]
        public string OppositeAccount { get; set; }

        [XmlArray(ElementName = "opposite", IsNullable = false)]
        [XmlArrayItem(ElementName = "line")]
        public string[] OppositeLines { get; set; }

        [XmlElement(ElementName = "amount", IsNullable = false)]
        public decimal Amount { get; set; }

        [XmlElement(ElementName = "curr", IsNullable = false)]
        public string CurrencySymbol { get; set; }

        [XmlElement(ElementName = "fee", IsNullable = false)]
        public decimal Fee { get; set; }

        [XmlElement(ElementName = "fee-curr", IsNullable = false)]
        public string FeeCurrencySymbol { get; set; }

        [XmlArray(ElementName = "details", IsNullable = false)]
        [XmlArrayItem(ElementName = "line")]
        public string[] DetailsLines { get; set; }

        [XmlElement(ElementName = "balance", IsNullable = false)]
        public decimal Balance { get; set; }

        [XmlElement(ElementName = "type", IsNullable = false)]
        public string Type { get; set; }
    }
}
