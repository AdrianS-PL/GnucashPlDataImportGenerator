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
    public class BossaCurrentInvestmentFundsDataFileTests
    {
        const string TestFile =
@"AGI004,20200925,130.5600,130.5600,130.5600,130.5600,0
AGI043,20200925,100.9700,100.9700,100.9700,100.9700,0
AGI044,20200925,116.7500,116.7500,116.7500,116.7500,0";

        [TestMethod]
        public void TestMethod1()
        {
            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(TestFile));

            var dataFile = BossaDataFile.CreateBossaFile<BossaCurrentInvestmentFundsDataFile>(stream);

            Assert.AreEqual(3, dataFile.Data.Count());
            Assert.IsNotNull(dataFile.Data.Single(q => q.FundSymbol == "AGI004" && q.Date == new DateTime(2020, 9, 25) && q.Price == 130.5600m));
            Assert.IsNotNull(dataFile.Data.Single(q => q.FundSymbol == "AGI043" && q.Date == new DateTime(2020, 9, 25) && q.Price == 100.9700m));
            Assert.IsNotNull(dataFile.Data.Single(q => q.FundSymbol == "AGI044" && q.Date == new DateTime(2020, 9, 25) && q.Price == 116.7500m));
        }
    }
}
