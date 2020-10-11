using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GnuCash.TransactionImportGenerator.Parsers
{
    abstract class XmlOperationsFileParser<ContractType> : IOperationsFileParser where ContractType : class
    {
        public abstract Encoding FileEncoding { get; }

        public abstract List<Operation> MapToOperationsFile(string xml);

        public bool CanParse(string xml)
        {
            bool success = true;
            try
            {
                ParseXml(xml);
            }
            catch
            {
                success = false;
            }

            return success;
        }

        /// <exception cref="System.Exception">Can throw various exceptions</exception>
        protected ContractType ParseXml(string xml)
        {
            ContractType result = null;

            XmlSerializer serializer = new XmlSerializer(typeof(ContractType));

            using (TextReader reader = new StringReader(xml))
            {
                result = (ContractType)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
