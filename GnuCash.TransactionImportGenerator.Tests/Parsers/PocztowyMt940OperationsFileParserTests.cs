using GnuCash.TransactionImportGenerator.Parsers.PocztowyMt940OperationsFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GnuCash.TransactionImportGenerator.Tests.Parsers
{
    [TestClass]
    public class PocztowyMt940OperationsFileParserTests
    {
        const string ValidFileContent =
@":20:MT940
:25:13201234/PL61175049066532561735311116
:60F:C210901PLN1234,18
:61:2108310902D469,99S0340000001090000123//123123
:86:034~
20Zakup kartą 1234 xxxx xxxx ~211234z dnia 2021.08.31;LEGO ~
22System A/S Billund~23~
24~25~
27BARTOSZ ORŁOWSKI ~28~
3018705216~31PL25187052162169878869775531~
32BARTOSZ ORŁOWSKI~33 ~
38PL25187052162169878869775531
:61:2109220922D208,36S0340000180203119123//1234123
:86:034~
20/TXT///GAZ FV VFP/000123123~21/21~
22~23~
24~25~
27PGNIG OBRÓT DETALICZNY SPÓŁKA Z OGR~28 ANICZONĄ ODPOWIEDZIALNOŚCIĄ
WARSZA~
3010201026~31PL59102010260000180203119773~
32PGNIG OBRÓT DETALICZNY SPÓŁ~33KA Z OGR ANICZONĄ ODPOWIEDZ~
38PL59102010260000180203119773
:61:2109170917C800,00S0341000009716123123//1234123
:86:034~
20na połowe wrzesnia~21~
22~23~
24~25~
27BARTOSZ ORŁOWSKI TRAUGUTTA 8B/9
92-67~286 POZNAŃ~
3010501924~31PL48212062184608634237069115~
32BARTOSZ ORŁOWSKI TRAUGUTTA 8B~33/9
92-676 POZNAŃ~
38PL61175049066532561735311116
:62F:C210930PLN2550,65
86:NAME ACCOUNT OWNER: BARTOSZ ORŁOWSKI
";

        [TestMethod]
        public void CanParse_Should_ReturnTrue_When_FileContentIsValid()
        {
            var parser = new PocztowyMt940OperationsFileParser();

            var result = parser.CanParse(ValidFileContent);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Should_CorrectlyParseFile_When_FileContentIsValid()
        {
            var parser = new PocztowyMt940OperationsFileParser();

            var result = parser.MapToOperationsFile(ValidFileContent);

            Assert.IsTrue(result.Count == 3);

            Assert.IsTrue(result.All(q => q.AccountCode == "61175049066532561735311116"));
            Assert.IsTrue(result.All(q => q.Currency == "PLN"));

            var operation1 = result.Single(q => q.Date == new DateTime(2021, 8, 31) && q.Amount == -469.99M);
            string description1 = "Rachunek przeciwstawny: 25187052162169878869775531\r\nDane kontrahenta: BARTOSZ ORŁOWSKI \r\nTytuł: Zakup kartą 1234 xxxx xxxx 1234z dnia 2021.08.31;LEGO System A/S Billund\r\n";
            Assert.AreEqual(description1, operation1.Description);

            var operation2 = result.Single(q => q.Date == new DateTime(2021, 9, 22) && q.Amount == -208.36M);
            string description2 = "Rachunek przeciwstawny: 59102010260000180203119773\r\nDane kontrahenta: PGNIG OBRÓT DETALICZNY SPÓŁKA Z OGR ANICZONĄ ODPOWIEDZIALNOŚCIĄWARSZA\r\nTytuł: /TXT///GAZ FV VFP/000123123/21\r\n";
            Assert.AreEqual(description2, operation2.Description);

            var operation3 = result.Single(q => q.Date == new DateTime(2021, 9, 17) && q.Amount == 800.00M);
            string description3 = "Rachunek przeciwstawny: 48212062184608634237069115\r\nDane kontrahenta: BARTOSZ ORŁOWSKI TRAUGUTTA 8B/992-676 POZNAŃ\r\nTytuł: na połowe wrzesnia\r\n";
            Assert.AreEqual(description3, operation3.Description);

        }
    }
}
