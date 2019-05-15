using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Domain;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IImportedFilesRepository _importedFilesRepository;

        public FileController(IImportedFilesRepository importedFilesRepository)
        {
            _importedFilesRepository = importedFilesRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImportedFile>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ListAllFiles()
        {
            var files = await _importedFilesRepository.GetFiles();

            return files.Any() ? Ok(files) : NoContent() as IActionResult;
        }
    }
}