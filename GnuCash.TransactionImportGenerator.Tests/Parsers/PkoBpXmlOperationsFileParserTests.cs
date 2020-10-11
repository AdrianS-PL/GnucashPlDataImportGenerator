using GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlOperationsFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Tests.Parsers
{
    [TestClass]
    public class PkoBpXmlOperationsFileParserTests
    {
        [TestMethod()]
        public void TestParseSingleOperationFile()
        {
            string fileContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
  <account-history>
  <search>  
    <account>1234</account>  
    <date since='2017-12-21' to='2017-12-31' />     
       <filtering>Wszystkie</filtering>
  </search>
     <operations>
    <operation>
    <exec-date>2017-12-30</exec-date>
    <order-date>2017-12-31</order-date>
    <type>Przelew z rachunku</type>
    <description>test description</description>
    <amount curr='PLN'>-70.50</amount>
    <ending-balance curr='PLN'>+18420.06</ending-balance>
  </operation>
</operations>
</account-history>
     ";


            var importer = new PkoBpXmlOperationsFileParser();

            var result = importer.MapToOperationsFile(fileContent);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1234", result[0].AccountCode);
            Assert.AreEqual(DateTime.Parse("2017-12-30"), result[0].Date);
            Assert.AreEqual("Przelew z rachunku test description", result[0].Description);
            Assert.AreEqual(-70.50M, result[0].Amount);
            Assert.AreEqual("PLN", result[0].Currency);
        }

        [TestMethod()]
        public void CanParseOnCardFileShouldReturnFalse()
        {
            string fileContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<paycard-operations-history>
<search>
  <card>**** **** **** 2633</card>
  <date since='2019-01-01' to='2019-01-31'/>
  <filtering>Wszystkie</filtering>
</search>
<operations>
  <operation>
    <exec-date>2019-01-30</exec-date>
    <order-date>2019-01-29</order-date>
    <type>Płatność kartą</type>
    <description>Tytuł: Transakcja</description>
    <amount curr='PLN'>-100.00</amount>
  </operation>
</operations>
</paycard-operations-history>
     ";

            var importer = new PkoBpXmlOperationsFileParser();

            bool result = importer.CanParse(fileContent);

            Assert.IsFalse(result);

        }

        [TestMethod]
        public void CanParseOnValidFileShouldReturnTrue()
        {
            string fileContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
  <account-history>
  <search>  
    <account>1234</account>  
    <date since='2017-12-21' to='2017-12-31' />     
       <filtering>Wszystkie</filtering>
  </search>
     <operations>
    <operation>
    <exec-date>2017-12-30</exec-date>
    <order-date>2017-12-31</order-date>
    <type>Przelew z rachunku</type>
    <description>test description</description>
    <amount curr='PLN'>-70.50</amount>
    <ending-balance curr='PLN'>+18420.06</ending-balance>
  </operation>
</operations>
</account-history>
     ";

            var importer = new PkoBpXmlOperationsFileParser();

            bool result = importer.CanParse(fileContent);

            Assert.IsTrue(result);

        }
    }
}
