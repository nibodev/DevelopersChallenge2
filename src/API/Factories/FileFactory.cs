using System.IO;
using API.Domain;
using API.Services;
using Microsoft.AspNetCore.Http;

namespace API.Factories
{
    public static class FileFactory
    {
        public static ImportedFile Create(IFormFile file)
        {
            ImportedFile importedFile;
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                importedFile = new ImportedFile(file.FileName, stream);
            }

            new OsfParser(importedFile).Parse();

            return importedFile;
        }
    }
}