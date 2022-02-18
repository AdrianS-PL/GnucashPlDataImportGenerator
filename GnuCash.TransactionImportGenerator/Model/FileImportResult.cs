using GnuCash.DataModel.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Model
{
    public class FileImportResult : FileImportResultBase
    {
        public List<Operation> FileData { get; set; }
    }
}
