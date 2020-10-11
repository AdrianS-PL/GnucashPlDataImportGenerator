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
    public class PkoBpAccountOperationsXmlFileSearch
    {
        [XmlElement(ElementName = "account", IsNullable = false)]
        public string Account { get; set; }

        [XmlElement(ElementName = "filtering", IsNullable = false)]
        public string Filtering { get; set; }

        [XmlElement(ElementName = "date", IsNullable = false)]
        public PkoBpAccountOperationsXmlFileSearchDate Date { get; set; }
    }    
}
