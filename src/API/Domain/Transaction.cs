using System;

namespace API.Domain
{
    public class Transaction
    {
        public Guid Id { get; protected set; }
        public TransactionType Type { get; protected set; }
        public string Description { get; protected set; }
        public decimal Ammount { get; protected set; }
        public DateTime Date { get; protected set; }
        public ImportedFile File { get; protected set; }

        protected Transaction() { }

        public Transaction(TransactionType type, decimal value, DateTime date, string memo)
        {
            Type = type;
            Description = memo;
            Ammount = Math.Abs(value);
            Date = date;
        }

        public Transaction WithFile(ImportedFile importedFile)
        {
            File = importedFile;
            return this;
        }
    }
}