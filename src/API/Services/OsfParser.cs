using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using API.Domain;

namespace API.Services
{
    public class OsfParser
    {
        private readonly ImportedFile _importedFile;

        public OsfParser(ImportedFile importedFile)
        {
            _importedFile = importedFile;
        }

        public void Parse()
        {
            using (var stream = new StreamReader(_importedFile.FileContent))
            {
                bool inTag = false;
                var stack = new Stack<string>();
                var tagBuikder = new StringBuilder();
                var valueBuilder = new StringBuilder();

                while (stream.Peek()>0)
                {
                    var car = stream.Read();
                    switch ( car )
                    {
                        case '<':
                            inTag = true;
                            break;
                        case '>':
                            stack.Push(tagBuikder.ToString());
                            inTag = false;
                            break;
                        case '/':
                            break;
                        default:
                            if (inTag)
                                tagBuikder.Append(car);
                            else
                                valueBuilder.Append(car);
                            break;
                    }

                }
            }
        }
    }
}