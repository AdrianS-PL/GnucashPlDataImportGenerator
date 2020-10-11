using GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Tests.Parsers
{
    [TestClass]
    public class IngCsvOperationsFileParserTests
    {
        [TestMethod]
        public void TestParseSingleOperationFile()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";
""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";

            var parser = new IngCsvOperationsFileParser();

            var result = parser.MapToOperationsFile(fileContent);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("11 1930 1901 2583 0446 5641 4160", result[0].AccountCode);
            Assert.AreEqual(DateTime.Parse("2020-09-19"), result[0].Date);
            Assert.AreEqual("Dane kontrahenta:  Katarzyna Testowa  Tytuł: Przelew na test Rachunek przeciwstawny: '79883410191150313681000043 ' PRZELEW  ", result[0].Description);
            Assert.AreEqual(-2435.34M, result[0].Amount);
            Assert.AreEqual("PLN", result[0].Currency);
        }

        [TestMethod()]
        public void CanParseOnValidFileShouldReturnTrue()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";
""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";


            var parser = new IngCsvOperationsFileParser();

            Assert.IsTrue(parser.CanParse(fileContent));
        }

        [TestMethod()]
        public void CanParse_Should_ReturnFalse_When_FirstLineStartsWithWrongString()
        {
            string fileContent =
@"""Lista operacji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";
""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";

            var parser = new IngCsvOperationsFileParser();

            Assert.IsFalse(parser.CanParse(fileContent));
        }

        [TestMethod()]
        public void CanParse_Should_ReturnFalse_When_FirstLineDoesNotContainIngString()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www..pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";
""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";


            var parser = new IngCsvOperationsFileParser();

            Assert.IsFalse(parser.CanParse(fileContent));
        }

        [TestMethod()]
        public void CanParse_Should_ReturnFalse_When_AccountsHeaderLineDoesNotExist()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";


            var parser = new IngCsvOperationsFileParser();

            Assert.IsFalse(parser.CanParse(fileContent));
        }

        [TestMethod()]
        public void CanParse_Should_ReturnFalse_When_AccountLineIsEmpty()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";


            var parser = new IngCsvOperationsFileParser();

            Assert.IsFalse(parser.CanParse(fileContent));
        }

        [TestMethod()]
        public void CanParse_Should_ReturnFalse_When_NextAccountLineIsNotEmpty()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";
""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";
""Testowe Konto 2 (PLN)"";;""98 8017 0002 4375 5294 3454 4314"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

""Data transakcji"";""Data księgowania"";""Dane kontrahenta"";""Tytuł"";""Nr rachunku"";""Nazwa banku"";""Szczegóły"";""Nr transakcji"";""Kwota transakcji (waluta rachunku)"";""Waluta"";""Kwota blokady/zwolnienie blokady"";""Waluta"";""Kwota płatności w walucie"";""Waluta"";""Saldo po transakcji"";""Waluta"";;;;;
2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";


            var parser = new IngCsvOperationsFileParser();

            Assert.IsFalse(parser.CanParse(fileContent));
        }

        [TestMethod()]
        public void CanParse_Should_ReturnFalse_When_OperationsHeaderLineDoesNotExist()
        {
            string fileContent =
@"""Lista transakcji"";;;;;""ING Bank Śląski S.A. ul. Sokolska 34, 40-086 Katowice www.ing.pl"";;;;;;;;;;;;;;;
""Dokument nr 0012300000_041020"";
""Wygenerowany dnia: 2020-10-04, 19:51"";;;;;;;;;;;;;;;;;;;;

""Dane Użytkownika:"";
""JAN KOWALSKI, TESTOWA 5B/45 11-111 GDAŃSK"";

""Wybrane rachunki:"";
""Testowe Konto (PLN)"";;""11 1930 1901 2583 0446 5641 4160"";

""Zastosowane kryteria wyboru"";;;;;""Podsumowanie"";;

""Zakres dat:"";""2020-07-01 - 2020-09-30"";""Typy transakcji:"";""wszystkie"";;""Liczba transakcji:"";1;

;;;;;""Suma uznań (24):"";3847,16;PLN;;;;;;;;;;;;;

;;;;;""Suma obciążeń (11):"";4049,22;PLN;;;;;;;;;;;;;

2020-09-19;2020-09-21;"" Katarzyna Testowa "";""Przelew na test"";'79883410191150313681000043 ';""Bank Spółdzielczy Poznań"";""PRZELEW  "";'202012345678191234';-2435,34;PLN;;;;;4000,00;PLN;;;;;


""Dokument ma charakter informacyjny, nie stanowi dowodu księgowego"";
";


            var parser = new IngCsvOperationsFileParser();

            Assert.IsFalse(parser.CanParse(fileContent));
        }
    }
}
