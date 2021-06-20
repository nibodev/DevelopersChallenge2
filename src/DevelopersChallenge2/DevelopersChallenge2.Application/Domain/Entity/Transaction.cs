using DevelopersChallenge2.Application.Domain.Enum;
using System;

namespace DevelopersChallenge2.Application.Domain.Entity
{
    public class Transaction
    {
        public string Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime PostedDate { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public string OfxFileReference { get; set; }

        public Transaction(decimal amount, DateTime postedDate, TransactionType transactionType,
            string memo, string ofxReference)
        {
            Amount = amount;
            PostedDate = postedDate;
            TransactionType = transactionType;
            Memo = memo;
            OfxFileReference = ofxReference;
        }

        public Transaction()
        {

        }
    }
}
