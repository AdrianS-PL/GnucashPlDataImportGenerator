using FakeItEasy;
using GnuCash.DataModel.Dtos;
using GnuCash.DataModel.Queries;
using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;
using Microsoft.ML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator.Tests.OperationTransferAccountPrediction.MlNet
{
    [TestClass]
    public class MlNetCategoryPredictionModelTrainerTests
    {
        [TestMethod()]
        public async Task When_GivenSimpleInputData_Should_CreateModelWithOver90PercentAccuracy()
        {

            var operations = new List<TransferAccountPredictionDataDto>();

            string account1Guid = "account1Guid";
            string account2Guid = "account2Guid";
            string defaultAccountGuid = "defaultAccountGuid";

            for (int i = 0; i < 50; i++)
            {
                var o = new TransferAccountPredictionDataDto()
                {
                    Description = "Test income 111111",
                    TransferAccountGuid = account1Guid
                };
                operations.Add(o);
            }

            for (int i = 0; i < 50; i++)
            {
                var o = new TransferAccountPredictionDataDto()
                {
                    Description = "Test income 222222",
                    TransferAccountGuid = account2Guid
                };
                operations.Add(o);
            }

            var operationForPrediction1 = new Operation() { Description = "Test income 111111" };
            var operationForPrediction2 = new Operation() { Description = "Test income 222222" };
            var operationForPrediction3 = new Operation() { Description = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" };

            var fakeTrainingDataQuery = A.Fake<ITransferAccountPredictionDataQuery>();
            A.CallTo(() => fakeTrainingDataQuery.GetTransferAccountPredictionDataAsync()).Returns(operations);

            MlNetOperationTransferAccountPredictor trainer = new MlNetOperationTransferAccountPredictor(fakeTrainingDataQuery, 
                new EveryFifthForTestingSplitStrategy<MlNetPredictionInputRow, MlNetPreSplitPredictionInputRow>(new MlNetPreSplitMappingProfile()));

            var modelData = await trainer.CreatePredictionModel();


            Assert.IsTrue(modelData.MacroAccuracyPercent > 90);
            Assert.IsTrue(modelData.MicroAccuracyPercent > 90);
            Assert.AreEqual(account1Guid, modelData.PredictTransferAccount(operationForPrediction1, defaultAccountGuid, 0.8f));
            Assert.AreEqual(account2Guid, modelData.PredictTransferAccount(operationForPrediction2, defaultAccountGuid, 0.8f));
            Assert.AreEqual(defaultAccountGuid, modelData.PredictTransferAccount(operationForPrediction3, defaultAccountGuid, 0.8f));
        }

        [TestMethod()]
        public async Task When_GivenLargeSimpleInputData_Should_CreateModelWithOver90PercentAccuracy()
        {

            var operations = new List<TransferAccountPredictionDataDto>();

            string account1Guid = Guid.NewGuid().ToString();
            string account2Guid = Guid.NewGuid().ToString();

            for (int i = 0; i < 5000; i++)
            {
                var o = new TransferAccountPredictionDataDto()
                {
                    Description = "Test income 111111",
                    TransferAccountGuid = account1Guid
                };
                operations.Add(o);
            }

            for (int i = 0; i < 5000; i++)
            {
                var o = new TransferAccountPredictionDataDto()
                {
                    Description = "Test income 222222",
                    TransferAccountGuid = account2Guid
                };
                operations.Add(o);
            }

            var fakeTrainingDataQuery = A.Fake<ITransferAccountPredictionDataQuery>();
            A.CallTo(() => fakeTrainingDataQuery.GetTransferAccountPredictionDataAsync()).Returns(operations);

            MlNetOperationTransferAccountPredictor trainer = new MlNetOperationTransferAccountPredictor(fakeTrainingDataQuery,
                new EveryFifthForTestingSplitStrategy<MlNetPredictionInputRow, MlNetPreSplitPredictionInputRow>(new MlNetPreSplitMappingProfile()));

            var modelData = await trainer.CreatePredictionModel();

            Assert.IsTrue(modelData.MacroAccuracyPercent > 90);
            Assert.IsTrue(modelData.MicroAccuracyPercent > 90);
        }

        [TestMethod()]
        public async Task When_TrainingDataLenghtIsZero_Should_ReturnEmptyPredictionModel()
        {
            var fakeTrainingDataQuery = A.Fake<ITransferAccountPredictionDataQuery>();
            A.CallTo(() => fakeTrainingDataQuery.GetTransferAccountPredictionDataAsync()).Returns(new List<TransferAccountPredictionDataDto>());

            MlNetOperationTransferAccountPredictor trainer = new MlNetOperationTransferAccountPredictor(fakeTrainingDataQuery,
                new EveryFifthForTestingSplitStrategy<MlNetPredictionInputRow, MlNetPreSplitPredictionInputRow>(new MlNetPreSplitMappingProfile()));

            var modelData = await trainer.CreatePredictionModel();

            Assert.IsTrue(modelData is EmptyPredictionModel);
        }

        [TestMethod()]
        public async Task When_SplitStrategyReturnedTrainingDataLenghtIsZero_Should_ReturnEmptyPredictionModel()
        {
            var operations = new List<TransferAccountPredictionDataDto>();

            string account1Guid = Guid.NewGuid().ToString();

            for (int i = 0; i < 50; i++)
            {
                var o = new TransferAccountPredictionDataDto()
                {
                    Description = "Test income 111111",
                    TransferAccountGuid = account1Guid
                };
                operations.Add(o);
            }

            var emptyAfterDataSplitDataSet = new List<MlNetPredictionInputRow>();
            var notEmptyAfterDataSplitDataSet = new List<MlNetPredictionInputRow>() { new MlNetPredictionInputRow() };

            var fakeTrainingDataQuery = A.Fake<ITransferAccountPredictionDataQuery>();
            A.CallTo(() => fakeTrainingDataQuery.GetTransferAccountPredictionDataAsync()).Returns(operations);

            var fakeSplitStrategy = A.Fake<ITrainingAndTestDataSplitStrategy<MlNetPreSplitPredictionInputRow>>();
            A.CallTo(() => fakeSplitStrategy.SplitTrainingAndTestData(A<IEnumerable<MlNetPreSplitPredictionInputRow>>.Ignored))
                .Returns((new MLContext().Data.LoadFromEnumerable(notEmptyAfterDataSplitDataSet), new MLContext().Data.LoadFromEnumerable(emptyAfterDataSplitDataSet)));

            MlNetOperationTransferAccountPredictor trainer = new MlNetOperationTransferAccountPredictor(fakeTrainingDataQuery, fakeSplitStrategy);

            var modelData = await trainer.CreatePredictionModel();

            Assert.IsTrue(modelData is EmptyPredictionModel);
        }

        [TestMethod()]
        public async Task When_SplitStrategyReturnedEvaluationDataLenghtIsZero_Should_ReturnEmptyPredictionModel()
        {
            var operations = new List<TransferAccountPredictionDataDto>();

            string account1Guid = Guid.NewGuid().ToString();

            for (int i = 0; i < 50; i++)
            {
                var o = new TransferAccountPredictionDataDto()
                {
                    Description = "Test income 111111",
                    TransferAccountGuid = account1Guid
                };
                operations.Add(o);
            }

            var emptyAfterDataSplitDataSet = new List<MlNetPredictionInputRow>();
            var notEmptyAfterDataSplitDataSet = new List<MlNetPredictionInputRow>() { new MlNetPredictionInputRow() };

            var fakeTrainingDataQuery = A.Fake<ITransferAccountPredictionDataQuery>();
            A.CallTo(() => fakeTrainingDataQuery.GetTransferAccountPredictionDataAsync()).Returns(operations);

            var fakeSplitStrategy = A.Fake<ITrainingAndTestDataSplitStrategy<MlNetPreSplitPredictionInputRow>>();
            A.CallTo(() => fakeSplitStrategy.SplitTrainingAndTestData(A<IEnumerable<MlNetPreSplitPredictionInputRow>>.Ignored))
                .Returns((new MLContext().Data.LoadFromEnumerable(emptyAfterDataSplitDataSet), new MLContext().Data.LoadFromEnumerable(notEmptyAfterDataSplitDataSet)));

            MlNetOperationTransferAccountPredictor trainer = new MlNetOperationTransferAccountPredictor(fakeTrainingDataQuery, fakeSplitStrategy);

            var modelData = await trainer.CreatePredictionModel();

            Assert.IsTrue(modelData is EmptyPredictionModel);
        }
    }
}
