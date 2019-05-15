using System.IO;
using API.Domain;
using Microsoft.AspNetCore.Http;

namespace API.Factories
{
    public static class FileFactory
    {
        public static ImportedFile Create(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                return new ImportedFile(file.FileName, stream);
            }
        }
    }
}