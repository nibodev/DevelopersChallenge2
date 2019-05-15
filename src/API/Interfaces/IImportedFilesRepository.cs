using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;

namespace API.Interfaces
{
    public interface IImportedFilesRepository
    {
        Task<IEnumerable<ImportedFile>> GetFiles();
    }
}