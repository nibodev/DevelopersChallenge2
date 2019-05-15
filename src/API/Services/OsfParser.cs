using System.Collections.Generic;
using System.IO;
using API.Domain;

namespace API.Services
{
    public class OsfParser
    {
        public IEnumerable<BankStamentLine> Parse(Stream stream)
        {
            if(stream==null) throw new InvalidDataException("No data to process");

            return new BankStamentLine[]{};
        }
    }
}