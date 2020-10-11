using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Model
{
    public class FileImportResult
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public Exception Exception { get; set; }
        public string Pathname { get; set; }
        public List<Operation> FileData { get; set; }
    }
}
