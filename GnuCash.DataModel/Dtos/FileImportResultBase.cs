using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.DataModel.Dtos
{
    public class FileImportResultBase
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public Exception Exception { get; set; }
        public string Pathname { get; set; }
    }
}
