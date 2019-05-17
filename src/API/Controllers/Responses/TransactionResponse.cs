using System;
using System.ComponentModel;
using API.Domain;

namespace API.Controllers.Responses
{
    public class TransactionResponse
    {
        public TransactionResponse(Guid id, AccountResponse account, DateTime date, TransactionType type, string description, decimal value, bool isReconciled)
        {
            Id = id;
            Account = account;
            Date = date;
            Type = type;
            Description = description;
            Value = value;
            IsReconciled = isReconciled;
        }

        public Guid Id { get; }
        public AccountResponse Account { get; }
        public DateTime Date { get; }
        public TransactionType Type { get; set; }
        public string Description { get;  }
        public decimal Value { get; }
        public bool IsReconciled { get; }
    }
}