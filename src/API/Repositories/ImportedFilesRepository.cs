using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DataContext;
using API.Domain;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ImportedFilesRepository : IImportedFilesRepository
    {
        private readonly ReconcileContext _context;

        public ImportedFilesRepository(ReconcileContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ImportedFile>> GetFiles()
        {
            return await _context.ImportedFiles.Include(x=>x.BankAccount).ToListAsync();
        }

        public async Task Add(ImportedFile importedFile)
        {
            await _context.ImportedFiles.AddAsync(importedFile);
            await _context.SaveChangesAsync();
        }

        public async Task<ImportedFile> Get(string fileFileName)
        {
            return await _context.ImportedFiles.Where(x => x.FileName == fileFileName)
                .Include(x=>x.BankAccount)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> TransactionDoesntExist(Transaction transaction)
        {
            return !(await _context.Transactions.Where(
                    x => x.Ammount == transaction.Ammount && x.Date == transaction.Date &&
                         x.Description == transaction.Description && x.Type == transaction.Type &&
                         x.File.FileName != transaction.File.FileName)
                .AnyAsync());
        }
    }
}