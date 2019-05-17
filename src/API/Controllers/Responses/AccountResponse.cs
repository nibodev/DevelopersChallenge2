namespace API.Controllers.Responses
{
    public class AccountResponse
    {
        public AccountResponse(string account, string bank)
        {
            Account = account;
            Bank = bank;
        }

        public string Bank { get; }
        public string Account { get; }
    }
}