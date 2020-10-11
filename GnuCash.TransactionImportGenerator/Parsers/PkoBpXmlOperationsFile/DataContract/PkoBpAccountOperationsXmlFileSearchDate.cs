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
    public class PkoBpAccountOperationsXmlFileSearchDate
    {
        [XmlAttribute(AttributeName = "since")]
        public DateTime Since { get; set; }

        [XmlAttribute(AttributeName = "to")]
        public DateTime To { get; set; }
    }
}
