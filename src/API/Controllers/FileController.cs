using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Domain;
using API.Factories;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

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

        [HttpPut]
        [ActionName("uploadfile")]
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var uploadFile = FileFactory.Create(file);
            var importedFile = await _importedFilesRepository.Get(file.FileName);

            var workingFile = importedFile?.UpdateContent(uploadFile.FileContent) ?? uploadFile;

            await _importedFilesRepository.Add(workingFile);

            return Accepted();
        }
    }
}