using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using API.Domain;

namespace API.Services
{
    public class OsfParser
    {
        private readonly ImportedFile _importedFile;
        private enum FragmentType
        {
            Header,
            OpeningTag,
            Value,
            ClosingTag
        }

        public OsfParser(ImportedFile importedFile)
        {
            _importedFile = importedFile;
        }

        public void Parse()
        {
            var transactionsVisitor = new GetTransactionsVisitor();
            var ofx = CreateDataTreeStructure();

            ofx.Accept(transactionsVisitor);

            foreach (var transaction in transactionsVisitor.Transactions)
            {
                _importedFile.AddTransaction(transaction);
            }
        }

        private Node CreateDataTreeStructure()
        {
            using (var stream = new StringReader(_importedFile.FileContent))
            {
                var currentFragmentType = FragmentType.Header;
                var stack = new Stack<Node>();
                var builder = new StringBuilder();
                var currentNode = default(Node);

                while (stream.Peek() > 0)
                {
                    var car = (char)stream.Read();
                    Trace.WriteLine($"{currentFragmentType}-{builder.ToString()}");
                    switch (car)
                    {
                        case '<':
                            if (currentFragmentType == FragmentType.Value)
                            {
                                var value = builder.ToString();
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    currentNode?.SetValue(value);
                                    stack.Pop();
                                    currentNode = stack.Peek();
                                    Trace.WriteLine($"Pop {currentNode}");
                                }
                            }

                            currentFragmentType = FragmentType.OpeningTag;
                            builder.Clear();
                            break;
                        case '>':
                            switch (currentFragmentType)
                            {
                                case FragmentType.OpeningTag:
                                    currentNode = new Node(builder.ToString(), currentNode);
                                    stack.Push(currentNode);
                                    Trace.WriteLine($"Push {currentNode}");
                                    break;
                                case FragmentType.ClosingTag:
                                    currentNode = stack.Pop();
                                    currentNode = stack.Any() ? stack.Peek() : currentNode;
                                    Trace.WriteLine($"Pop {currentNode}");
                                    break;
                            }
                            currentFragmentType = FragmentType.Value;
                            builder.Clear();
                            break;
                        case '/':
                            if (currentFragmentType == FragmentType.OpeningTag)
                                currentFragmentType = FragmentType.ClosingTag;
                            else
                                builder.Append(car);
                            break;
                        case (char) 10:
                        case (char) 13:
                            break;
                        default:
                            builder.Append(car);
                            break;
                    }
                }
                return currentNode;
            }
        }
    }
}