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
    }
}