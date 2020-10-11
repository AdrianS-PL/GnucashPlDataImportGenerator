using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Parsers
{
    public interface IOperationsFileParser
    {
        Encoding FileEncoding { get; }
        List<Operation> MapToOperationsFile(string fileContent);
        bool CanParse(string fileContent);
    }
}
