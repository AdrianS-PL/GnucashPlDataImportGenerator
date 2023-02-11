using FakeItEasy;
using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Dtos;
using GnuCash.DataModel.Queries;
using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator.Tests
{
    [TestClass]
    public class TransactionImportFileDataGeneratorTests
    {
        [TestMethod]
        public async Task Should_ReturnEmptyResult_When_InputIsEmpty()
        {
            var transferAccountPredictor = A.Fake<IOperationTransferAccountPredictor>();
            var predictionModel = A.Fake<ITrainedPredictionModel>();
            var gnuCashContext = PrepareFakeContext(GetAccountsData());
            A.CallTo(() => transferAccountPredictor.CreatePredictionModel()).Returns(predictionModel);

            var fileDataGenerator = new TransactionImportFileDataGenerator(transferAccountPredictor, gnuCashContext);

            var result = await fileDataGenerator.GenerateImportFileData(new List<Operation>(), new List<OperationPair>());

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public async Task Should_CorrectlyMapOperations()
        {
            var operations = GetOperationsData();
            var operationsCopy = GetOperationsData();

            var operationPairs = new List<OperationPair>()
            {
                new OperationPair()
                {
                    Operation1 = operations[0],
                    Operation2 = operations[1]
                }
            };

            var transferAccountPredictor = A.Fake<IOperationTransferAccountPredictor>();
            var predictionModel = A.Fake<ITrainedPredictionModel>();
            var accountsData = GetAccountsData();
            var gnuCashContext = PrepareFakeContext(accountsData);
            A.CallTo(() => transferAccountPredictor.CreatePredictionModel()).Returns(predictionModel);
            A.CallTo(() => predictionModel.PredictTransferAccount(operations[2], A<string>.Ignored, 0.5f)).Returns(accountsData[4].guid);
            A.CallTo(() => predictionModel.PredictTransferAccount(operations[3], A<string>.Ignored, 0.5f)).Returns(accountsData[5].guid);

            var fileDataGenerator = new TransactionImportFileDataGenerator(transferAccountPredictor, gnuCashContext);

            var result = await fileDataGenerator.GenerateImportFileData(operations, operationPairs);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("KONTO 1", result[0].Account);
            Assert.AreEqual("KONTO 2", result[0].TransferAccount);
            Assert.AreEqual(operationsCopy[0].Amount, result[0].Deposit);
            Assert.AreEqual(operationsCopy[0].Description, result[0].Description);
            Assert.AreEqual(operationsCopy[0].Description, result[0].Memo);
            Assert.AreEqual(operationsCopy[1].Description, result[0].TransferMemo);
            Assert.AreEqual(operationsCopy[0].Date, result[0].Date);

            Assert.AreEqual("KONTO 1", result[1].Account);
            Assert.AreEqual("KONTO 3", result[1].TransferAccount);
            Assert.AreEqual(operationsCopy[2].Amount, result[1].Deposit);
            Assert.AreEqual(operationsCopy[2].Description, result[1].Description);
            Assert.IsNull(result[1].Memo);
            Assert.IsNull(result[1].TransferMemo);
            Assert.AreEqual(operationsCopy[2].Date, result[1].Date);

            Assert.AreEqual("KONTO 1", result[2].Account);
            Assert.AreEqual("KONTO 4", result[2].TransferAccount);
            Assert.AreEqual(operationsCopy[3].Amount, result[2].Deposit);
            Assert.AreEqual(operationsCopy[3].Description, result[2].Description);
            Assert.IsNull(result[2].Memo);
            Assert.IsNull(result[2].TransferMemo);
            Assert.AreEqual(operationsCopy[3].Date, result[2].Date);
        }

        private static GnuCashContext PrepareFakeContext(List<Account> accountData)
        {
            var gnuCashContext = A.Fake<GnuCashContext>();

            IQueryable<Account> fakeIQueryable = accountData.AsQueryable();
            var accountsDbSet = A.Fake<DbSet<Account>>(d => d.Implements(typeof(IQueryable<Account>)).Implements(typeof(IAsyncEnumerable<Account>)));
            A.CallTo(() => ((IQueryable<Account>)accountsDbSet).GetEnumerator()).Returns(fakeIQueryable.GetEnumerator());
            A.CallTo(() => ((IQueryable<Account>)accountsDbSet).Provider).Returns(fakeIQueryable.Provider);
            A.CallTo(() => ((IQueryable<Account>)accountsDbSet).Expression).Returns(fakeIQueryable.Expression);
            A.CallTo(() => ((IQueryable<Account>)accountsDbSet).ElementType).Returns(fakeIQueryable.ElementType);
            A.CallTo(() => ((IAsyncEnumerable<Account>)accountsDbSet).GetAsyncEnumerator(A<CancellationToken>.Ignored))
                .Returns(fakeIQueryable.ToAsyncEnumerable().GetAsyncEnumerator(CancellationToken.None));

            A.CallTo(() => gnuCashContext.Set<Account>()).Returns(accountsDbSet);
            return gnuCashContext;
        }

        private static List<Operation> GetOperationsData()
        {
            var operations = new List<Operation>()
            {
                new Operation()
                {
                    AccountCode = "KONTO 1",
                    Amount = 100,
                    Currency = "PLN",
                    Date = new DateTime(2000, 1, 1),
                    Description = "DESC 1"
                },
                new Operation()
                {
                    AccountCode = "KONTO 2",
                    Amount = -100,
                    Currency = "PLN",
                    Date = new DateTime(2000, 1, 2),
                    Description = "DESC 2"
                },
                new Operation()
                {
                    AccountCode = "KONTO 1",
                    Amount = 1040,
                    Currency = "PLN",
                    Date = new DateTime(2000, 1, 3),
                    Description = "DESC 3"
                },
                new Operation()
                {
                    AccountCode = "KONTO 1",
                    Amount = 1020,
                    Currency = "PLN",
                    Date = new DateTime(2000, 1, 4),
                    Description = "DESC 4"
                }
            };

            return operations;
        }


        private static List<Account> GetAccountsData()
        {
            var list = new List<Account>()
            {
                new Account()
                {
                    code = "ROOT",
                    guid = "GUID1",
                    name = "ROOT"
                },
                new Account()
                {
                    code = "DEFAULT TRANSFER ACCOUNT",
                    guid = "GUID2",
                    name = "NIEPRZYPISANE"
                },
                new Account()
                {
                    code = "KONTO 1",
                    guid = "GUID3",
                    name = "KONTO 1"
                },
                new Account()
                {
                    code = "KONTO 2",
                    guid = "GUID4",
                    name = "KONTO 2"
                },
                new Account()
                {
                    code = "KONTO 3",
                    guid = "GUID5",
                    name = "KONTO 3"
                },
                new Account()
                {
                    code = "KONTO 4",
                    guid = "GUID6",
                    name = "KONTO 4"
                }
            };

            for(int i = 1; i < list.Count; i++)
            {
                list[i].Parent = list[0];
            }

            return list;
        }
    }
}
