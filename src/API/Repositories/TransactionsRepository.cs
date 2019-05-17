using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataContext;
using API.Domain;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ReconcileContext _context;

        public TransactionsRepository(ReconcileContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> MonthlyBalance(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            return await _context
                .Transactions.Where(x => x.Date >= startDate && x.Date < endDate)
                .Include(x=>x.File)
                .Include(x=>x.File.BankAccount)
                .ToListAsync();
        }

        public async Task<Transaction> Get(Guid id)
        {
            return await _context
                .Transactions.Where(x => x.Id == id)
                .Include(x => x.File)
                .Include(x => x.File.BankAccount)
                .FirstOrDefaultAsync();
        }

        public async Task Update()
        {
            await _context.SaveChangesAsync();
        }
    }
}