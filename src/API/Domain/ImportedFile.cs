using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace API.Domain
{
    public class ImportedFile
    {
        public Guid Id { get; protected set; }
        public string FileName { get; protected set; }
        public DateTime ImportDate { get; set; }
        public ImportStatus Status { get; protected set; }
        public string FileContent { get; protected set; }
        public IList<Transaction> Transactions { get; protected set; }

        protected ImportedFile()
        {
        }

        public ImportedFile(string fileName, StreamReader fileData)
        {
            Transactions = new List<Transaction>();
            ImportDate = DateTime.Now;
            Status = ImportStatus.Uploaded;
            FileName = fileName;
            FileContent = fileData.ReadToEnd();
        }

        public ImportedFile UpdateContent(string uploadFileFileContent)
        {
            FileContent = uploadFileFileContent;
            Status = ImportStatus.Uploaded;
            ImportDate = DateTime.Now;
            return this;
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction.WithFile(this));
        }
    }

    public enum TransactionType : short
    {
        Debit = 1,
        Credit = 2
    }
}