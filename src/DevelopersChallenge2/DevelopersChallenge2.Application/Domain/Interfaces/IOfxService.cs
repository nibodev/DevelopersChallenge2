using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Application.Domain.Interfaces
{
    public interface IOfxService
    {
        Task ProcessOfxFiles(List<IFormFile> formFiles);
    }
}
