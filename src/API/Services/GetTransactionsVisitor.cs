using System;
using System.Collections.Generic;
using System.Linq;
using API.Domain;

namespace API.Services
{
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
}