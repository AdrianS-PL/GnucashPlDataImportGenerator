using Microsoft.ML;
using System.Collections.Generic;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    abstract class TrainingAndTestDataSplitStrategy<PredictionInputType, PreSplitInputType> where PredictionInputType : class where PreSplitInputType : class
    {
        private readonly SimpleMapper<PredictionInputType, PreSplitInputType> _mapper;


        protected TrainingAndTestDataSplitStrategy(SimpleMapper<PredictionInputType, PreSplitInputType> simpleMapper)
        {
            _mapper = simpleMapper;
        }

        public abstract (IDataView trainingDataView, IDataView testDataView) SplitTrainingAndTestData(IEnumerable<PreSplitInputType> data);

        protected IEnumerable<PredictionInputType> Map(IEnumerable<PreSplitInputType> preSplitTypeData)
        {
            var result = new List<PredictionInputType>();

            foreach (var item in preSplitTypeData)
            {
                var mapped = _mapper.Map(item);
                result.Add(mapped);
            }

            return result;
        }
    }
}
