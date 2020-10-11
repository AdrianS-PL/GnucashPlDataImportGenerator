using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Tests.OperationTransferAccountPrediction.MlNet
{
    [TestClass]
    public class EmptyPredictionModelTests
    {
        [TestMethod]
        public void When_InputDataIsNull_Should_AlwaysReturnDefaultTransferAccountAndZeroAccuracy()
        {
            var model = new EmptyPredictionModel();
            string defaultAccount = "defaultAccount";

            Assert.AreEqual(defaultAccount, model.PredictTransferAccount(null, defaultAccount));
            Assert.AreEqual(0, model.MacroAccuracyPercent);
            Assert.AreEqual(0, model.MicroAccuracyPercent);
        }
    }
}
