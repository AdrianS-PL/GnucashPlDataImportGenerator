using GnuCash.TransactionImportGenerator.Parsers;
using GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile;
using GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlCardOperationsFile;
using GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlOperationsFile;
using GnuCash.TransactionImportGenerator.Parsers.PocztowyXmlOperationsFile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Tests.Parsers
{
    [TestClass]
    public class ParsersInteroperationTests
    {



        [TestMethod]
        public void CanParse_Should_ReturnTrueOnlyForCorrectFileForAllParsers()
        {
            var validFilesDictionary = new Dictionary<Type, string>
            {
                { typeof(PkoBpXmlOperationsFileParser), pkoBpAccountOperationsXmlFile },
                { typeof(PkoBpXmlCardOperationsFileParser), pkoBpCardOperationsXmlFile },
                { typeof(IngCsvOperationsFileParser), ingAccountOperationsCsvFile },
                { typeof(PocztowyXmlOperationsFileParser), pocztowyAccountOperationsXmlFile }
            };

            var parsers = GetParsersProvider().GetServices<IOperationsFileParser>();

            StringBuilder errorMessages = new StringBuilder();
            foreach(var parser in parsers)
            {
                if(!validFilesDictionary.ContainsKey(parser.GetType()))
                    errorMessages.AppendLine($"Error! Parser {parser.GetType().Name} has no test file defined");

                foreach (var validTypeEntry in validFilesDictionary)
                {
                    if(parser.CanParse(validTypeEntry.Value))
                    {
                        if (validTypeEntry.Key != parser.GetType())
                            errorMessages.AppendLine($"Error! Parser {parser.GetType().Name} can parse file meant for {validTypeEntry.Key.GetType().Name}");
                    }
                    else
                    {
                        if (validTypeEntry.Key == parser.GetType())
                            errorMessages.AppendLine($"Error! Parser {parser.GetType().Name} cannot parse its own file format");
                    }
                }
            }

            Assert.IsTrue(string.IsNullOrWhiteSpace(errorMessages.ToString()), errorMessages.ToString());
        }

        private IServiceProvider GetParsersProvider()
        {
            var services = new ServiceCollection();
            services.AddOperationsFileParsers();
            return services.BuildServiceProvider();
        }

        private const string pkoBpAccountOperationsXmlFile =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
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

        private const string pkoBpCardOperationsXmlFile =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
  <paycard-operations-history>
  <search>  
    <card>1234</card>  
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
  </operation>
</operations>
</paycard-operations-history>
     ";

        private const string pocztowyAccountOperationsXmlFile =
            @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
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

        private const string ingAccountOperationsCsvFile =
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
    }
}
