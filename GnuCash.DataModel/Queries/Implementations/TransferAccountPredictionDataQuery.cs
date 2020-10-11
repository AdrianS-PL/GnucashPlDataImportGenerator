using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.DatabaseModel.Enums;
using GnuCash.DataModel.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.DataModel.Queries.Implementations
{
    class TransferAccountPredictionDataQuery : GnuCashContextQueryBase, ITransferAccountPredictionDataQuery
    {
        private static string[] accountTypes = { AccountType.EXPENSE, AccountType.INCOME };

        public TransferAccountPredictionDataQuery(GnuCashContext context) : base(context)
        {
        }

        public List<TransferAccountPredictionDataDto> GetTransferAccountPredictionData()
        {
            return GetTransferAccountPredictionDataAsQueryable().ToList();
        }

        public IQueryable<TransferAccountPredictionDataDto> GetTransferAccountPredictionDataAsQueryable()
        {
            DateTime minDate = DateTime.Today.AddYears(-2);

            return Context.transactions.Where(q => q.post_date >= minDate && q.Splits.Any(r => accountTypes.Contains(r.AccountInstance.account_type) && r.AccountInstance.hidden != true && r.AccountInstance.placeholder != true))
                .Select(q => new
                {
                    Description = q.description,
                    Date = q.post_date,
                    SplitToTransferAccount = q.Splits.FirstOrDefault(r => accountTypes.Contains(r.AccountInstance.account_type) && r.AccountInstance.hidden != true && r.AccountInstance.placeholder != true),
                })

                .Select(q => new TransferAccountPredictionDataDto()
                {
                    Description = q.Description,
                    Date = q.Date ?? new DateTime(2000, 1, 1),
                    TransferAccountGuid = q.SplitToTransferAccount.AccountInstance.guid,
                    Amount = -((decimal)q.SplitToTransferAccount.value_num / q.SplitToTransferAccount.value_denom)
                }
            );
        }

        public async Task<List<TransferAccountPredictionDataDto>> GetTransferAccountPredictionDataAsync()
        {
            return await GetTransferAccountPredictionDataAsQueryable().ToListAsync();
        }
    }
}
