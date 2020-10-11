using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;
using Microsoft.ML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator.Tests.OperationTransferAccountPrediction.MlNet
{
    [TestClass]
    public class LastMonthDataAsTestDataSplitStrategyTests
    {
        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void When_InputDataIsNull_Should_ThrowNullReferenceException()
        {
            var strategy = new LastMonthDataAsTestDataSplitStrategy(new MlNetPreSplitMappingProfile());

            strategy.SplitTrainingAndTestData(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void When_InputDataIsEmpty_Should_ThrowNullReferenceException()
        {
            var data = new List<MlNetPreSplitPredictionInputRow>();

            var strategy = new LastMonthDataAsTestDataSplitStrategy(new MlNetPreSplitMappingProfile());

            strategy.SplitTrainingAndTestData(data);


        }

        [TestMethod()]
        public void When_AllDataIsInTheSameMonth_Should_ReturnEmptyTrainingData()
        {
            var data = new List<MlNetPreSplitPredictionInputRow>()
            {
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 1, 1) },
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 1, 2) }
            };

            var strategy = new LastMonthDataAsTestDataSplitStrategy(new MlNetPreSplitMappingProfile());

            var result = strategy.SplitTrainingAndTestData(data);
            Assert.AreEqual(data.Count, result.testDataView.Preview().RowView.Length);
            Assert.AreEqual(0, result.trainingDataView.Preview().RowView.Length);
        }

        [TestMethod()]
        public void When_DataIsCorrect_Should_ReturnCorrectSplit()
        {
            var data = new List<MlNetPreSplitPredictionInputRow>()
            {
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 1, 1) },
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 1, 2) },
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 2, 1) },
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 2, 2) },
                new MlNetPreSplitPredictionInputRow() { Date = new DateTime(2000, 2, 3) }
            };

            var strategy = new LastMonthDataAsTestDataSplitStrategy(new MlNetPreSplitMappingProfile());

            var result = strategy.SplitTrainingAndTestData(data);
            Assert.AreEqual(3, result.testDataView.Preview().RowView.Length);
            Assert.AreEqual(2, result.trainingDataView.Preview().RowView.Length);
        }
    }
}
