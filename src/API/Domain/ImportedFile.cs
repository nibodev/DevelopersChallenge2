using System;

namespace API.Domain
{
    public class ImportedFile
    {
        public Guid Id { get; protected set; }
        public string FileName { get; protected set; }
    }
}