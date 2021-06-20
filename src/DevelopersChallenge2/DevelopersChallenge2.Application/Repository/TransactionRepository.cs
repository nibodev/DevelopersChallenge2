using DevelopersChallenge2.Application.Domain.Entity;
using DevelopersChallenge2.Application.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Application.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MyContext dbContext;
        public TransactionRepository(MyContext context)
        {
            dbContext = context;
        }

        public void Add(Transaction transaction)
        {
            dbContext.Add(transaction);
            dbContext.SaveChanges();
        }

        public void Save(List<Transaction> transactions)
        {
            dbContext.AddRange(transactions);
            dbContext.SaveChanges();
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            return await dbContext.Transactions
                .OrderBy(x => x.PostedDate)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsWithoutDuplicates()
        {
            return await dbContext.Transactions
                .Select(x => new Transaction(x.Amount, x.PostedDate, x.TransactionType, x.Memo, x.OfxFileReference))
                .Distinct()
                .ToListAsync();
        }

        public void Add(System.Transactions.Transaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}
