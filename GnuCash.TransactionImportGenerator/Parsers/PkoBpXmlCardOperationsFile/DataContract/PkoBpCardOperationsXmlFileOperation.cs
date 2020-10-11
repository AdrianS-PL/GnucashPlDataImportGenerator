using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlCardOperationsFile.DataContract
{
    [ExcludeFromCodeCoverage]
    public class PkoBpCardOperationsXmlFileOperation
    {
        [XmlElement(ElementName = "exec-date", IsNullable = false)]
        public DateTime ExecutionDate { get; set; }

        [XmlElement(ElementName = "order-date", IsNullable = false)]
        public DateTime OrderDate { get; set; }

        [XmlElement(ElementName = "type", IsNullable = false)]
        public string Type { get; set; }

        [XmlElement(ElementName = "description", IsNullable = false)]
        public string Description { get; set; }

        [XmlElement(ElementName = "amount", IsNullable = false)]
        public PkoBpCardOperationsXmlFileOperationAmount Amount { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PkoBpCardOperationsXmlFileOperationAmount
    {
        [XmlText]
        public decimal Value { get; set; }

        [XmlAttribute(AttributeName = "curr")]
        public string CurrencySymbol { get; set; }
    }
}
