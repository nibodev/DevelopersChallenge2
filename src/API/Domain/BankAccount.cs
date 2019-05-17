using System.Collections.Generic;

namespace API.Domain
{
    public class BankAccount
    {
        public BankAccount(string banckId, string accountId, AccountType type)
        {
            BanckId = banckId;
            AccountId = accountId;
            Type = type;
            Id = $"{BanckId}{AccountId}";
        }

        public string Id { get; protected set; }
        public string BanckId { get; protected set; }
        public string AccountId { get; protected set; }
        public AccountType Type { get; protected set; }
        public IList<ImportedFile> Files { get; } = new List<ImportedFile>();
    }
}