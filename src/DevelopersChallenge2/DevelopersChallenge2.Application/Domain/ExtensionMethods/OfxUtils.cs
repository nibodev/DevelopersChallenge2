using DevelopersChallenge2.Application.Domain.Entity;
using DevelopersChallenge2.Application.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DevelopersChallenge2.Application.Domain.ExtensionMethods
{
    public static class OfxUtils
    {
        public static Ofx ToOfx(this string path)
        {
            var tags = GetTagsFromOfxFile(path);
            var transactions = BuildTransactions(tags);
            return new Ofx(transactions);            
        }

        private static List<Transaction> BuildTransactions(IEnumerable<string> tags)
        {
            var transactions = new List<Transaction>();

            Transaction transaction = new Transaction();
            foreach (var tag in tags)
            {
                if (tag.IndexOf("<STMTTRN>") != -1)
                {
                    transaction = new Transaction()
                    {
                        Id = Guid.NewGuid().ToString()
                    };
                    continue;
                }
                else if (tag.IndexOf("<TRNTYPE>") != -1)
                {
                    transaction.TransactionType = BuildTransactionType(tag.Replace("<TRNTYPE>", "").Trim());
                    continue;
                }
                else if (tag.IndexOf("<DTPOSTED>") != -1)
                {
                    string date = tag.Replace("<DTPOSTED>", "").Substring(0, 8);
                    DateTime postedDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    transaction.PostedDate = postedDate;
                    continue;
                }
                else if (tag.IndexOf("<TRNAMT>") != -1)
                {
                    decimal.TryParse(tag.Replace("<TRNAMT>", ""), out decimal amount);
                    transaction.Amount = amount;
                    continue;
                }
                else if (tag.IndexOf("<MEMO>") != -1)
                {
                    transaction.Memo = tag.Replace("<MEMO>", "");
                }
                transactions.Add(transaction);
                continue;
            }

            return transactions;
        }

        private static TransactionType BuildTransactionType(string type)
        {
            return (type == TransactionType.CREDIT.ToString()) ? TransactionType.CREDIT : TransactionType.DEBIT;
        }

        private static IEnumerable<string> GetTagsFromOfxFile(string path)
        {
            if (!File.Exists(path))
            {
                string[] createText = { "" };
                File.WriteAllLines(path, createText);
            }

            string appendText = "This is extra text" + Environment.NewLine;
            File.AppendAllText(path, appendText);

            return from line in File.ReadAllLines(path)
                   where line.Contains("<STMTTRN>") ||
                   line.Contains("<TRNTYPE>") ||
                   line.Contains("<DTPOSTED>") ||
                   line.Contains("<TRNAMT>") ||
                   line.Contains("<FITID>") ||
                   line.Contains("<CHECKNUM>") ||
                   line.Contains("<MEMO>")
                   select line;
        }
    }
}
