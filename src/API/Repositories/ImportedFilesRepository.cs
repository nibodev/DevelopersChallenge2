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
            return await _context.ImportedFiles.ToListAsync();
        }
    }
}