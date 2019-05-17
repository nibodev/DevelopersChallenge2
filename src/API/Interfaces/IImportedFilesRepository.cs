using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;

namespace API.Interfaces
{
    public interface IImportedFilesRepository
    {
        Task<IEnumerable<ImportedFile>> GetFiles();
        Task Add(ImportedFile importedFile);
        Task<ImportedFile> Get(string fileFileName);
        Task<bool> TransactionDoesntExist(Transaction transaction);
    }

    public interface IAccoutsRepository
    {
        Task<BankAccount> Get(string id);
    }

    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> MonthlyBalance(int year, int month);
        Task<Transaction> Get(Guid id);
        Task Update();
    }
}