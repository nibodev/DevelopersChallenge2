using System;
using System.Collections.Generic;

namespace DevelopersChallenge2.Application.Domain.Entity
{
    public class Ofx
    {
        public Ofx(List<Transaction> transactions)
        {
            Transactions = transactions;
        }

        public DateTime ServerDate { get; set; }
        public List<Transaction> Transactions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Account Account { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime LastTransactionDate { get; set; }
    }
}
