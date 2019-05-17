using API.Controllers.Responses;
using API.Domain;
using Xunit;

namespace Tests.API.Converters
{
    public class AccountMapToResponse
    {
        [Fact]
        public void Shoul_map_account_details()
        {
            var account = new BankAccount("bankId", "accountId", AccountType.Checking);
            var response = account.MapToResponse();

            Assert.Equal(account.AccountId,response.Account);
            Assert.Equal(account.BanckId, response.Bank);
        }
    }
}