using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator
{
    public class TransactionImportFileDataGenerator
    {
        private readonly IOperationTransferAccountPredictor _transferAccountPredictor;
        private readonly GnuCashContext _context;
        public TransactionImportFileDataGenerator(IOperationTransferAccountPredictor transferAccountPredictor, GnuCashContext context)
        {
            _transferAccountPredictor = transferAccountPredictor;
            _context = context;
        }

        public async Task<List<TransactionImportFileRow>> GenerateImportFileData(IList<Operation> operations,  IList<OperationPair> operationPairs)
        {
            foreach (var pair in operationPairs)
            {
                operations.Remove(pair.Operation1);
                operations.Remove(pair.Operation2);
            }

            var predictionModelData = await _transferAccountPredictor.CreatePredictionModel();

            var fileRows = new List<TransactionImportFileRow>();
            var accountFullNames = await _context.Set<Account>().GetAccountFullNamesAsync();
            foreach (var pair in operationPairs)
            {
                string account1Name = accountFullNames.SingleOrDefault(q => q.Code == pair.AccountCode1)?.FullName 
                    ?? throw new KeyNotFoundException($"Brak konta o kodzie: {pair.AccountCode1}");
                string account2Name = accountFullNames.SingleOrDefault(q => q.Code == pair.AccountCode2)?.FullName
                    ?? throw new KeyNotFoundException($"Brak konta o kodzie: {pair.AccountCode2}");

                var row = new TransactionImportFileRow()
                {
                    Account = account1Name,
                    Date = new DateTime[] { pair.Date1, pair.Date2 }.Min(),
                    Deposit = pair.Amount1,
                    Description = pair.Description1,
                    Memo = pair.Description1,
                    TransferAccount = account2Name,
                    TransferMemo = pair.Description2
                };
                fileRows.Add(row);
            }

            string defaultTransferAccountGuid = accountFullNames.SingleOrDefault(q => q.Code == "DEFAULT TRANSFER ACCOUNT")?.Guid
                    ?? throw new KeyNotFoundException($"Brak konta o kodzie: DEFAULT TRANSFER ACCOUNT");

            foreach (var operation in operations)
            {
                var guid = predictionModelData.PredictTransferAccount(operation, defaultTransferAccountGuid);

                string account1Name = accountFullNames.SingleOrDefault(q => q.Code == operation.AccountCode)?.FullName
                    ?? throw new KeyNotFoundException($"Brak konta o kodzie: {operation.AccountCode}");
                string account2Name = accountFullNames.SingleOrDefault(q => q.Guid == guid)?.FullName
                    ?? throw new KeyNotFoundException($"Brak konta o GUID: {guid}");

                var row = new TransactionImportFileRow()
                {
                    Account = account1Name,
                    Date = operation.Date,
                    Deposit = operation.Amount,
                    Description = operation.Description,
                    TransferAccount = account2Name
                };
                fileRows.Add(row);
            }

            return fileRows;
        }
    }
}
