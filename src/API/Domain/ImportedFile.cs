using System;
using System.Collections.Generic;
using System.IO;

namespace API.Domain
{
    public class ImportedFile
    {
        public Guid Id { get; protected set; }
        public string FileName { get; protected set; }
        public DateTime ImportDate { get; set; }
        public string FileContent { get; protected set; }
        public IList<Transaction> Transactions { get; protected set; }
        public BankAccount BankAccount { get; set; }

        protected ImportedFile()
        {
        }

        protected ImportedFile(string fileName)
        {
            Transactions = new List<Transaction>();
            ImportDate = DateTime.Now;
            FileName = fileName;
        }

        public ImportedFile(string fileName, StreamReader fileData) : this(fileName)
        {
            FileContent = fileData.ReadToEnd();
        }

        public ImportedFile(ImportedFile file) : this(file.FileName)
        {
            FileContent = file.FileContent;
        }

        public ImportedFile UpdateContent(string uploadFileFileContent)
        {
            FileContent = uploadFileFileContent;
            ImportDate = DateTime.Now;
            return this;
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction.WithFile(this));
        }

        public void SetAccount(BankAccount bankAccount)
        {
            BankAccount = bankAccount;
        }
    }
}