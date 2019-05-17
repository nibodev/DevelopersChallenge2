using System;
using API.Services;

namespace API.Controllers.Responses
{
    public class FileResponse
    {
        public FileResponse(Guid id, string name, DateTime uploadDate, AccountResponse account)
        {
            Id = id;
            Name = name;
            UploadDate = uploadDate;
            Account = account;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime UploadDate { get; }
        public AccountResponse Account { get; }
    }
}