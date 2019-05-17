using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using API.Controllers.Responses;
using API.Domain;
using Xunit;

namespace Tests.API.Converters
{
    public class FieMapTpResponse
    {
        [Fact]
        public void Shoul_map_file_to_response()
        {
            var file = new ImportedFile("FileName", StreamReader.Null);
            file.SetAccount(new BankAccount("bid","aid",AccountType.Checking));

            var response = file.MapToResponse();

            Assert.Equal( file.Id, response.Id);
            Assert.Equal(file.FileName, response.Name);
            Assert.Equal(file.ImportDate, response.UploadDate);
            Assert.Equal(file.BankAccount.AccountId, response.Account.Account);
            Assert.Equal(file.BankAccount.BanckId, response.Account.Bank);
        }
    }
}
