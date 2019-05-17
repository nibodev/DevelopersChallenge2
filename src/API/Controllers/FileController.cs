using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Controllers.Responses;
using API.Domain;
using API.Factories;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IImportedFilesRepository _importedFilesRepository;
        private readonly IAccoutsRepository _accoutsRepository;

        public FileController(IImportedFilesRepository importedFilesRepository, IAccoutsRepository accoutsRepository)
        {
            _importedFilesRepository = importedFilesRepository;
            _accoutsRepository = accoutsRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ImportedFile>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ListAllFiles()
        {
            var files = (await _importedFilesRepository.GetFiles()).Select(x=>x.MapToResponse());

            return files.Any() ? Ok(files) : NoContent() as IActionResult;
        }

        [HttpPut]
        [ActionName("UploadFile")]
        [ProducesResponseType(typeof(FileResponse), (int) HttpStatusCode.Accepted)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            //TODO This shoul go to an application class
            var importedFile = await _importedFilesRepository.Get(file.FileName);

            if (importedFile != null) return Accepted(importedFile.MapToResponse());

            var uploadFile = FileFactory.Create(file);
            

            var newImport = new ImportedFile(uploadFile);
            var account = await _accoutsRepository.Get(uploadFile.BankAccount.Id);
            newImport.SetAccount(account ?? uploadFile.BankAccount);

            foreach (var transaction in uploadFile.Transactions)
            {
                if (await _importedFilesRepository.TransactionDoesntExist(transaction))
                    newImport.AddTransaction(transaction);
            }

            await _importedFilesRepository.Add(newImport);

            return Accepted(newImport.MapToResponse());
        }
    }
}