using DevelopersChallenge2.Application.Domain.Entity;
using DevelopersChallenge2.Application.Domain.ExtensionMethods;
using DevelopersChallenge2.Application.Domain.Interfaces;
using DevelopersChallenge2.Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Application.Services
{
    public class OfxService : IOfxService
    {
        private readonly ILogger _logger;
        private readonly ITransactionRepository _transactionRepository;

        public OfxService(ILogger<OfxService> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public async Task ProcessOfxFiles(List<IFormFile> formFiles)
        {
            //TODO improve log
            long size = formFiles.Sum(f => f.Length);

            foreach (var formFile in formFiles)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetFileName(formFile.FileName);
                    var filePath = Path.Combine(Path.GetTempPath() + fileName + "-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss"));
                    //var filePath = Path.GetTempFileName();

                    using (var stream = File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    PersistsOfxFile(filePath);
                }
            }
        }

        private void PersistsOfxFile(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            _logger.LogInformation($"Start of ofx file conversion process. {fileName}");

            var ofxFile = filePath.ToOfx();
            if(ofxFile.Transactions != null)
            {
                var transactions = ofxFile.Transactions
                    .Select(x => { x.OfxFileReference = fileName; return x; })
                    .ToList();
                _transactionRepository.Save(transactions);
            }            
            
            _logger.LogInformation($"End of ofx file conversion process. {fileName}");
        }
    }
}
