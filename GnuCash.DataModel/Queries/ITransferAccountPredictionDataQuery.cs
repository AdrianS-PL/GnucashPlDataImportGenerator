using GnuCash.DataModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.DataModel.Queries
{
    public interface ITransferAccountPredictionDataQuery : IQuery
    {
        List<TransferAccountPredictionDataDto> GetTransferAccountPredictionData();
        Task<List<TransferAccountPredictionDataDto>> GetTransferAccountPredictionDataAsync();

        IQueryable<TransferAccountPredictionDataDto> GetTransferAccountPredictionDataAsQueryable();        
    }
}
