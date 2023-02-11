using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    abstract class PreSplitDataMappingProfile<PredictionInputType, PreSplitInputType> : Profile
    {
        protected PreSplitDataMappingProfile()
        {
            GetMap(CreateMap<PreSplitInputType, PredictionInputType>());
        }

        protected virtual IMappingExpression<PreSplitInputType, PredictionInputType> GetMap(IMappingExpression<PreSplitInputType, PredictionInputType> expression)
        {
            return expression;
        }
    }
}
