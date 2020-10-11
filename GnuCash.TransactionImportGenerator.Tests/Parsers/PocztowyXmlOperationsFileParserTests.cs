using GnuCash.TransactionImportGenerator.Parsers.PocztowyXmlOperationsFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Tests.Parsers
{
    [TestClass]
    public class PocztowyXmlOperationsFileParserTests
    {
        [TestMethod]
        public void TestParseSingleOperationFile()
        {
            string xml = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<acc_history xmlns=""http://api.bpsa.pl/historiaTransakcji"">
    <acc>1767843</acc>
    <acc-curr>PLN</acc-curr>
    <date-from>2019-02-01</date-from>
    <date-to>2019-02-28</date-to>
    <opening-balance>1701.61</opening-balance>
    <closing-balance>2877.90</closing-balance>
    <operation>
        <document-nr>6269</document-nr>
        <decree-nr>1</decree-nr>
        <transaction-date>2019-02-28</transaction-date>
        <order-date>2019-02-27</order-date>
        <proc-date>2019-02-28</proc-date>
        <acc>1767843</acc>
        <opposite-acc>23102010</opposite-acc>
        <opposite>
            <line>MPWiK</line>
        </opposite>
        <amount>-72.51</amount>
        <curr>PLN</curr>
        <fee>0.00</fee>
        <fee-curr>PLN</fee-curr>
        <details>
            <line>Symbol klienta:</line>
            <line>x</line>
        </details>
        <balance>2877.90</balance>
        <type>ZLECENIE STAŁE</type>
    </operation>	
</acc_history>";


            var importer = new PocztowyXmlOperationsFileParser();

            var result = importer.MapToOperationsFile(xml);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("1767843", result[0].AccountCode);
            Assert.AreEqual("PLN", result[0].Currency);
            Assert.AreEqual(new DateTime(2019, 2, 27), result[0].Date);
            Assert.AreEqual(-72.51M, result[0].Amount);
            Assert.AreEqual(@$"ZLECENIE STAŁE Rachunek przeciwstawny: 23102010{
                Environment.NewLine}Dane kontrahenta: MPWiK{
                Environment.NewLine}Tytuł: Symbol klienta: x{
                Environment.NewLine}", result[0].Description);
        }

        [TestMethod]
        public void TestParseTwoOperationsFile()
        {
            string xml = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<acc_history xmlns=""http://api.bpsa.pl/historiaTransakcji"">
    <acc>1767843</acc>
    <acc-curr>PLN</acc-curr>
    <date-from>2019-02-01</date-from>
    <date-to>2019-02-28</date-to>
    <opening-balance>1701.61</opening-balance>
    <closing-balance>2877.90</closing-balance>
    <operation>
        <document-nr>6269</document-nr>
        <decree-nr>1</decree-nr>
        <transaction-date>2019-02-28</transaction-date>
        <order-date>2019-02-27</order-date>
        <proc-date>2019-02-28</proc-date>
        <acc>1767843</acc>
        <opposite-acc>23102010</opposite-acc>
        <opposite>
            <line>MPWiK</line>
        </opposite>
        <amount>-72.51</amount>
        <curr>PLN</curr>
        <fee>0.00</fee>
        <fee-curr>PLN</fee-curr>
        <details>
            <line>Symbol klienta:</line>
            <line>x</line>
        </details>
        <balance>2877.90</balance>
        <type>ZLECENIE STAŁE</type>
    </operation>
	<operation>
        <document-nr>43456</document-nr>
        <decree-nr>1</decree-nr>
        <transaction-date>2019-02-04</transaction-date>
        <order-date>2019-02-04</order-date>
        <proc-date>2019-02-04</proc-date>
        <acc>1767843</acc>
        <opposite-acc>23102010</opposite-acc>
        <opposite>
            <line>MPWiK</line>
        </opposite>
        <amount>-72.55</amount>
        <curr>PLN</curr>
        <fee>0.00</fee>
        <fee-curr>PLN</fee-curr>
        <details>
            <line>Symbol klienta: </line>
            <line>x</line>
        </details>
        <balance>1629.06</balance>
        <type>PRZELEW WYCHODZĄCY</type>
    </operation>	
</acc_history>";

            var importer = new PocztowyXmlOperationsFileParser();

            var result = importer.MapToOperationsFile(xml);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
