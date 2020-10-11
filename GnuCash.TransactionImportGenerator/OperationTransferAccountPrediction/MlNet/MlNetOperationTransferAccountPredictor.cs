using GnuCash.TransactionImportGenerator.Model;
using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;
using Microsoft.ML;
using System.IO;
using Microsoft.ML.Data;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    class MlNetOperationTransferAccountPredictor : IOperationTransferAccountPredictor
    {
        ITransferAccountPredictionDataQuery _getTrainingDataQuery;
        ITrainingAndTestDataSplitStrategy<MlNetPreSplitPredictionInputRow> _splitStrategy;
        public MlNetOperationTransferAccountPredictor(ITransferAccountPredictionDataQuery getTrainingDataQuery,
            ITrainingAndTestDataSplitStrategy<MlNetPreSplitPredictionInputRow>  splitStrategy)
        {
            _getTrainingDataQuery = getTrainingDataQuery;
            _splitStrategy = splitStrategy;
        }

        public async Task<ITrainedPredictionModel> CreatePredictionModel()
        {
            var trainingOperations = (await _getTrainingDataQuery.GetTransferAccountPredictionDataAsync()).Select(q => new MlNetPreSplitPredictionInputRow()
            { Description = q.Description, Date = q.Date, TransferAccountId = q.TransferAccountGuid.ToString() });

            if (trainingOperations.Count() == 0)
                return new EmptyPredictionModel();

            MLContext ml = new MLContext();

            (IDataView trainingDataView, IDataView testDataView) = _splitStrategy.SplitTrainingAndTestData(trainingOperations);

            if(trainingDataView.Preview().RowView.Length == 0 || testDataView.Preview().RowView.Length == 0)
                return new EmptyPredictionModel();

            var processDataPipeline = ProcessData(ml);
            var trainedModel = BuildAndTrainModel(ml, trainingDataView, processDataPipeline);
            (double microAccuracy, double macroAccuracy) = Evaluate(ml, testDataView, trainedModel);

            byte[] trainedModelData;

            using (MemoryStream ms = new MemoryStream())
            {
                ml.Model.Save(trainedModel, trainingDataView.Schema, ms);
                trainedModelData = ms.ToArray();
            }

            return new TrainedPredictionModel(trainedModelData, microAccuracy * 100, macroAccuracy * 100);
        }

        private static IEstimator<ITransformer> ProcessData(MLContext ml)
        {
            var processDataPipeline = ml.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(MlNetPredictionInputRow.TransferAccountId), outputColumnName: "Label")
                .Append(ml.Transforms.Text.FeaturizeText(inputColumnName: nameof(MlNetPredictionInputRow.Description), outputColumnName: "DescriptionFeaturized"))
                .Append(ml.Transforms.Concatenate("Features", "DescriptionFeaturized"))
                .AppendCacheCheckpoint(ml);

            return processDataPipeline;
        }

        private static TransformerChain<Microsoft.ML.Transforms.KeyToValueMappingTransformer> BuildAndTrainModel(MLContext ml, IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            var trainer = ml.MulticlassClassification.Trainers.SdcaMaximumEntropy();

            var trainingPipeline = pipeline.Append(trainer)
                .Append(ml.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = trainingPipeline.Fit(trainingDataView);
            return trainedModel;
        }

        private static (double microAccuracy, double macroAccuracy) Evaluate(MLContext ml, IDataView testDataView, TransformerChain<Microsoft.ML.Transforms.KeyToValueMappingTransformer> trainedModel)
        {
            var testMetrics = ml.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));
            return (testMetrics.MicroAccuracy, testMetrics.MacroAccuracy);
        }
    }
}
