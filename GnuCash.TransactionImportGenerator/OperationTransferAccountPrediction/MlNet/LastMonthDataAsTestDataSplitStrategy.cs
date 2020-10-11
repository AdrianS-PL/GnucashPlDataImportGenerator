using Castle.Core.Internal;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    class LastMonthDataAsTestDataSplitStrategy : TrainingAndTestDataSplitStrategy<MlNetPredictionInputRow, MlNetPreSplitPredictionInputRow>,
        ITrainingAndTestDataSplitStrategy<MlNetPreSplitPredictionInputRow>
    {
        public LastMonthDataAsTestDataSplitStrategy(MlNetPreSplitMappingProfile mappingProfile) : base(mappingProfile)
        {
        }

        public override (IDataView trainingDataView, IDataView testDataView) SplitTrainingAndTestData(IEnumerable<MlNetPreSplitPredictionInputRow> data)
        {
            MLContext ml = new MLContext();

            data = data ?? throw new NullReferenceException($"{nameof(data)} argument cannot be null");

            if (data.IsNullOrEmpty())
                throw new ArgumentException($"{nameof(data)} cannot be empty");

            DateTime maxDate = data.Max(q => q.Date);
            DateTime firstDayOfLastMonth = new DateTime(maxDate.Year, maxDate.Month, 1);

            var testData = data.Where(q => q.Date >= firstDayOfLastMonth);
            var trainingData = data.Where(q => q.Date < firstDayOfLastMonth);



            IDataView trainingDataView = ml.Data.LoadFromEnumerable(Map(trainingData).ToList());
            IDataView testDataView = ml.Data.LoadFromEnumerable(Map(testData).ToList());

            return (trainingDataView, testDataView);
        }
    }
}
