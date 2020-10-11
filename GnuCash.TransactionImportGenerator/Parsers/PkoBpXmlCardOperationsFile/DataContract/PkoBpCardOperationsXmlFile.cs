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
    [XmlRoot(ElementName = "paycard-operations-history", IsNullable = false)]
    public class PkoBpCardOperationsXmlFile
    {
        [XmlElement(ElementName = "search", IsNullable = false)]
        public PkoBpCardOperationsXmlFileSearch SearchParameters { get; set; }

        [XmlArray(ElementName = "operations", IsNullable = false)]
        [XmlArrayItem(ElementName = "operation")]
        public PkoBpCardOperationsXmlFileOperation[] Operations { get; set; }
    }
}
