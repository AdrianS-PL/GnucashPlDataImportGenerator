using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlOperationsFile.DataContract
{
    [ExcludeFromCodeCoverage]
    [XmlRoot(ElementName = "account-history", IsNullable = false)]
    public class PkoBpAccountOperationsXmlFile
    {
        [XmlElement(ElementName = "search", IsNullable = false)]
        public PkoBpAccountOperationsXmlFileSearch SearchParameters { get; set; }

        [XmlArray(ElementName = "operations", IsNullable = false)]
        [XmlArrayItem(ElementName = "operation")]
        public PkoBpAccountOperationsXmlFileOperation[] Operations { get; set; }
    }
}
