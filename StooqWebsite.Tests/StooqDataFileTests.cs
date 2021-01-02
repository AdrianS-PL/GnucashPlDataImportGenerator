using Microsoft.VisualStudio.TestTools.UnitTesting;
using StooqWebsite.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace StooqWebsite.Tests
{
    [TestClass]
    public class StooqDataFileTests
    {
        const string fileContent =
            @"Data,Otwarcie,Najwyzszy,Najnizszy,Zamkniecie
2014-10-27,3.3186,3.333,3.3115,3.324
2014-10-28,3.3241,3.3306,3.3062,3.3064";

        [TestMethod]
        public void ParseFileTest()
        {
            using var stream = new MemoryStream(Encoding.ASCII.GetBytes(fileContent));

            var dataFile = new StooqDataFile(stream);

            Assert.AreEqual(2, dataFile.Data.Count());
            Assert.IsNotNull(dataFile.Data.Single(q => q.Date == new DateTime(2014, 10, 27) && q.OpeningPrice == 3.3186m &&
             q.MaxPrice == 3.333m && q.MinPrice == 3.3115m && q.ClosingPrice == 3.324m));
            Assert.IsNotNull(dataFile.Data.Single(q => q.Date == new DateTime(2014, 10, 28) && q.OpeningPrice == 3.3241m &&
             q.MaxPrice == 3.3306m && q.MinPrice == 3.3062m && q.ClosingPrice == 3.3064m));
        }
    }
}
