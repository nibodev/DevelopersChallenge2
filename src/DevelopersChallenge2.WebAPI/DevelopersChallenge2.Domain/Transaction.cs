using System;

namespace DevelopersChallenge2.Domain
{
    public class Transaction : Entity
    {
        public string TransactionType { get; set; }
        public DateTime DatePosted { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Memo { get; set; }

        public virtual int IdBankList { get; set; }
    }
}
