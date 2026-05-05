using System.Collections.Generic;
using System.Linq;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    internal abstract class SimpleMapper<PredictionInputType, PreSplitInputType> where PredictionInputType : class where PreSplitInputType : class
    {
        public abstract PredictionInputType Map(PreSplitInputType input);
        public  IEnumerable<PredictionInputType> Map(IEnumerable<PreSplitInputType> input)
        {
            var result = input.Select(Map);
            return result;
        }
    }
}
