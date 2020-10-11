using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Model
{
    public class OperationFilesParsingResult
    {
        public List<FileImportResult> ParsingResults { get; set; } = new List<FileImportResult>();

        public List<Operation> LoadedOperations { get; set; } = new List<Operation>();

        public IEnumerable<Operation> PairableOperations => LoadedOperations.Where(q => LoadedOperations.Any(r => r.Amount == -q.Amount && r != q && r.AccountCode != q.AccountCode));
    }
}
