using System;
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
                bool inTag = false;
                bool inValue = false;
                var stack = new Stack<Node>();
                var tagBuikder = new StringBuilder();
                var valueBuilder = new StringBuilder();
                var currentNode = default(Node);

                while (stream.Peek() > 0)
                {
                    var car = (char)stream.Read();
                    switch (car)
                    {
                        case '<':
                            if (inValue && valueBuilder.Length > 0)
                            {
                                currentNode?.SetValue(valueBuilder.ToString());
                                currentNode = stack.Pop();
                                Trace.WriteLine($"Pop {currentNode}");
                                currentNode = stack.Peek();
                            }
                            valueBuilder = new StringBuilder();
                            inTag = true;
                            inValue = false;
                            break;
                        case '>':
                            if (inTag)
                            {
                                currentNode = new Node(tagBuikder.ToString(), currentNode);
                                tagBuikder = new StringBuilder();
                                stack.Push(currentNode);
                                Trace.WriteLine($"Push {currentNode}");
                            }
                            inTag = false;
                            inValue = true;
                            break;
                        case '/':
                            if (stack.Count > 0)
                            {
                                currentNode = stack.Pop();
                                Trace.WriteLine($"Pop {currentNode}");
                                currentNode = stack.Count > 0 ? stack.Peek() : currentNode;
                            }
                            inTag = false;
                            break;
                        case (char)10:
                        case (char)13:
                            break;
                        default:
                            if (inTag)
                                tagBuikder.Append(car);
                            else if (inValue)
                                valueBuilder.Append(car);
                            break;
                    }
                }
                return currentNode;
            }
        }
    }

    internal class Node
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();

        public Node(string label, Node parent)
        {
            Label = label;
            parent?.Children.Add(this);
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void Accept(INodeVisitor visitor)
        {
            foreach (var child in Children)
            {
                child.Accept(visitor);
            }
            visitor.Visit(this);
        }

        public override string ToString() => $"{Label} : {Value}";
    }

    internal interface INodeVisitor
    {
        void Visit(Node node);
    }

    internal class GetTransactionsVisitor : INodeVisitor
    {
        public List<Transaction> Transactions { get; } = new List<Transaction>();

        public void Visit(Node node)
        {
            if (node.Label.Equals("STMTTRN"))
            {
                var value = Convert.ToDecimal(node.Children.Single(x => x.Label.Equals("TRNAMT")).Value);
                var type = node.Children.Single(x => x.Label.Equals("TRNTYPE")).Value.Equals("DEBIT") ? TransactionType.Debit : TransactionType.Credit;
                var date = node.Children.Single(x => x.Label.Equals("DTPOSTED")).Value.ParseDate();
                var memo = node.Children.Single(x => x.Label.Equals("MEMO")).Value;
                Transactions.Add(new Transaction(type, value, date, memo));
            }
        }
    }

    internal static class ConvertionExtensions
    {
        public static DateTime ParseDate(this string date)
        {
            var year = date.Substring(0, 4);
            var month = date.Substring(4, 2);
            var day = date.Substring(6, 2);
            var hour = date.Substring(8, 2);
            var minute = date.Substring(10, 2);
            var second = date.Substring(12, 2);
            var offset = Convert.ToInt32(date.Substring(15, 3));

            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var timeOffset = easternZone.BaseUtcOffset.Add(TimeSpan.FromDays(offset));
            

            return DateTime.Parse($"{year}-{month}-{day} {hour}:{minute}:{second}").Add(timeOffset);
        }
    }
}