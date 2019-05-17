using System.Linq;
using API.Domain;

namespace API.Services
{
    internal class GetAccountVisitor : INodeVisitor
    {
        public BankAccount BankAccount { get; protected set; }
        public void Visit(Node node)
        {
            if (node.Label.Equals("BANKACCTFROM"))
            {
                var bankId = node.Children.Single(x => x.Label == "BANKID").Value;
                var accountId = node.Children.Single(x => x.Label == "ACCTID").Value;
                var accountType = node.Children.Single(x => x.Label == "ACCTTYPE").Value;

                var type = accountType.Equals("CHECKING")
                    ? AccountType.Checking
                    : AccountType.Other;

                BankAccount = new BankAccount(bankId,accountId,type);
            }
        }
    }
}