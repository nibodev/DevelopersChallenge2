using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevelopersChallenge2.Domain
{
    public class BankList : Entity
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
