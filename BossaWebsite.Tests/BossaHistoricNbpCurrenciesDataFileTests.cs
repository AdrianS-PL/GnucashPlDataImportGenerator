using BossaWebsite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BossaWebsite.Tests
{
    [TestClass]
    public class BossaHistoricNbpCurrenciesDataFileTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BossaWebsite.Tests.mstnbp.zip");

            var dataFile = BossaDataFile.CreateBossaFile<BossaHistoricNbpCurrenciesDataFile>(stream);

            Assert.AreEqual(3, dataFile.Data.Count);
            Assert.IsNotNull(dataFile.Data["EUR"].Single(q => q.Date == new DateTime(1999, 01, 01) && q.Price == 4.0925m));
            Assert.IsNotNull(dataFile.Data["GBP"].Single(q => q.Date == new DateTime(1993, 01, 05) && q.Price == 2.3892m));
            Assert.IsNotNull(dataFile.Data["USD"].Single(q => q.Date == new DateTime(1993, 01, 06) && q.Price == 1.5912m));
        }
    }
}
