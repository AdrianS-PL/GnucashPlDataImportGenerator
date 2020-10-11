using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    class EveryFifthForTestingSplitStrategy<PredictionInputType, PreSplitInputType> :
        TrainingAndTestDataSplitStrategy<PredictionInputType, PreSplitInputType>, ITrainingAndTestDataSplitStrategy<PreSplitInputType> 
        where PredictionInputType : class where PreSplitInputType : class

    {
        public EveryFifthForTestingSplitStrategy(PreSplitDataMappingProfile<PredictionInputType, PreSplitInputType> mappingProfile) : base(mappingProfile)
        {
        }

        public override (IDataView trainingDataView, IDataView testDataView) SplitTrainingAndTestData(IEnumerable<PreSplitInputType> data)
        {
            MLContext ml = new MLContext();

            var inputData = data.ToList();

            var testData = new List<PreSplitInputType>();

            for (int i = 0; i < inputData.Count; i += 5)
            {
                testData.Add(inputData[i]);
            }

            var trainingData = inputData.Except(testData).ToList();

            IDataView trainingDataView = ml.Data.LoadFromEnumerable(Map(trainingData));
            IDataView testDataView = ml.Data.LoadFromEnumerable(Map(testData));

            return (trainingDataView, testDataView);
        }
    }
}
