using BossaWebsite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BossaWebsite.Tests
{
    [TestClass]
    public class BossaCurrentNbpCurrenciesDataFileTests
    {
        const string NbpTestFile =
@"EUR,20200930,4.5268,4.5268,4.5268,4.5268,0
GBP,20200930,4.9560,4.9560,4.9560,4.9560,0
USD,20200930,3.8658,3.8658,3.8658,3.8658,0";

        [TestMethod]
        public void TestMethod1()
        {
            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(NbpTestFile));

            var dataFile = BossaDataFile.CreateBossaFile<BossaCurrentNbpCurrenciesDataFile>(stream);

            Assert.AreEqual(3, dataFile.Data.Count());
            Assert.IsNotNull(dataFile.Data.Single(q => q.Currency == "EUR" && q.Date == new DateTime(2020, 9, 30) && q.Price == 4.5268m));
            Assert.IsNotNull(dataFile.Data.Single(q => q.Currency == "GBP" && q.Date == new DateTime(2020, 9, 30) && q.Price == 4.9560m));
            Assert.IsNotNull(dataFile.Data.Single(q => q.Currency == "USD" && q.Date == new DateTime(2020, 9, 30) && q.Price == 3.8658m));
        }
    }
}
