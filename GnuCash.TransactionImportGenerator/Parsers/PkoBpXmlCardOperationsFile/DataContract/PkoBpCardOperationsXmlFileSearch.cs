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
    public class PkoBpCardOperationsXmlFileSearch
    {
        [XmlElement(ElementName = "card", IsNullable = false)]
        public string Card { get; set; }

        [XmlElement(ElementName = "filtering", IsNullable = false)]
        public string Filtering { get; set; }

        [XmlElement(ElementName = "date", IsNullable = false)]
        public PkoBpCardOperationsXmlFileSearchDate Date { get; set; }
    }
}
