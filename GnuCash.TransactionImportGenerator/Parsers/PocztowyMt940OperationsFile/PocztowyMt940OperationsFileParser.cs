using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace GnuCash.TransactionImportGenerator.Parsers.PocztowyMt940OperationsFile
{
    class PocztowyMt940OperationsFileParser : IOperationsFileParser
    {
        public const string HeaderAccountTag = ":25:";
        public const string HeaderOpeningBalanceTag = ":60F:";
        public const string TransactionTag = "\r\n:61:";
        public const string TransactionDetailsTag = "\r\n:86:";
        public const string ExpectedFileBeginning = ":20:MT940\r\n:25:1320";
        public const string FooterClosingBalanceTag = "\r\n:62F:";
        public const string SubfieldsSeparator = "~";
        public const int CRLFLength = 2;

        private readonly string[] TransactionTitleCodes = { "20", "21", "22", "23", "24", "25", "26"};
        private readonly string[] TransactionAddressCodes = { "27", "28", "29"};
        private const string BeneficiaryAccountCode = "31";

        public Encoding FileEncoding => CodePagesEncodingProvider.Instance.GetEncoding(1250);

        public bool CanParse(string fileContent)
        {
            return fileContent.StartsWith(ExpectedFileBeginning);
        }

        public List<Operation> MapToOperationsFile(string fileContent)
        {
            int headerAccountTagIndex = fileContent.IndexOf(HeaderAccountTag);
            int accountLineEndIndex = fileContent.IndexOf("\r\n", headerAccountTagIndex);

            string account = fileContent.Substring(headerAccountTagIndex + 15, accountLineEndIndex - headerAccountTagIndex - 15);

            int headerOpeningBalanceTagIndex = fileContent.IndexOf(HeaderOpeningBalanceTag);

            string currency = fileContent.Substring(headerOpeningBalanceTagIndex + 12, 3);

            int firstTransactionPartStartIndex = fileContent.IndexOf(TransactionTag);
            int footerStartIndex = fileContent.IndexOf(FooterClosingBalanceTag);

            string transactionsContent = fileContent.Substring(firstTransactionPartStartIndex, footerStartIndex - firstTransactionPartStartIndex);

            transactionsContent = transactionsContent.TrimEnd('\n', '\r');

            string[] transactionsParts = transactionsContent.Split(TransactionTag, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<Operation>();

            foreach (string transactionContent in transactionsParts)
            {
                (string transactionPart, string transactionDetailsPart) = SplitTransactionBlock(transactionContent);

                var operation = ParseTransaction(transactionPart);
                operation.Description = ParseTransactionDetailsAndReturnDescription(transactionDetailsPart);
                operation.AccountCode = account;
                operation.Currency = currency;

                result.Add(operation);
            }

            return result;
        }

        private (string transactionPart, string transactionDetailsPart) SplitTransactionBlock(string transactionBlock)
        {
            string[] tmp = transactionBlock.Split(TransactionDetailsTag, StringSplitOptions.RemoveEmptyEntries);
            return (tmp[0], tmp[1]);
        }

        private static Operation ParseTransaction(string transactionPart)
        {
            const int valueDateYearIndex = 0;
            const int valueDateMonthIndex = 2;
            const int valueDateDayIndex = 4;
            const int creditDebitIndicatorIndex = 10;

            int valueDateYear = int.Parse(transactionPart.Substring(valueDateYearIndex, 2)) + 2000;
            int valueDateMonth = int.Parse(transactionPart.Substring(valueDateMonthIndex, 2));
            int valueDateDay = int.Parse(transactionPart.Substring(valueDateDayIndex, 2));

            int amountCommaIndex = transactionPart.IndexOf(',');

            string amountString = transactionPart.Substring(creditDebitIndicatorIndex + 1, amountCommaIndex - creditDebitIndicatorIndex + 2);

            decimal amount = decimal.Parse(amountString, CultureInfo.GetCultureInfo("pl"));
            if (transactionPart[creditDebitIndicatorIndex] == 'D')
                amount = -amount;

            return new Operation()
            {
                Amount = amount,
                Date = new DateTime(valueDateYear, valueDateMonth, valueDateDay)
            };
        }

        private string ParseTransactionDetailsAndReturnDescription(string transactionDetailsPart)
        {
            transactionDetailsPart = transactionDetailsPart.Replace("\r", "");
            transactionDetailsPart = transactionDetailsPart.Replace("\n", "");

            string[] subfieldsStrings = transactionDetailsPart.Split(SubfieldsSeparator, StringSplitOptions.RemoveEmptyEntries);
            string[] subfieldsStringsWithoutTransactionCode = subfieldsStrings.Skip(1).ToArray();

            var transactionTitle = new StringBuilder();
            var transactionAddressData = new StringBuilder();
            string beneficiaryAccount = "";

            foreach (string subfield in subfieldsStringsWithoutTransactionCode)
            {
                if (TransactionTitleCodes.Any(q => subfield.StartsWith(q)))
                {
                    transactionTitle.Append(subfield.AsSpan(2));
                }
                else if (TransactionAddressCodes.Any(q => subfield.StartsWith(q)))
                {
                    transactionAddressData.Append(subfield.AsSpan(2));
                }
                else if (subfield.StartsWith(BeneficiaryAccountCode) && subfield.Length > BeneficiaryAccountCode.Length)
                {
                    //usunięcie PL z numeru konta
                    beneficiaryAccount = subfield.Substring(4);
                }
            }

            StringBuilder sb = new StringBuilder();

            if (beneficiaryAccount.Length > 0)
            { 
                sb.Append("Rachunek przeciwstawny: ");
                sb.AppendLine(beneficiaryAccount);
            }

            sb.Append("Dane kontrahenta: ");
            sb.AppendLine(transactionAddressData.ToString());

            sb.Append("Tytuł: ");
            sb.AppendLine(transactionTitle.ToString());

            return sb.ToString();

        }
    }
}
