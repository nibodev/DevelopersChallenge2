using System;
using API.Domain;

namespace API.Controllers.Responses
{
    public static class MappingExtensions
    {
        public static TransactionResponse MapToResponse(this Transaction transaction)
        {
            return new TransactionResponse(
                id: transaction.Id,
                date: transaction.Date,
                account: transaction.File.BankAccount.MapToResponse(),
                type: transaction.Type,
                description: transaction.Description,
                value: transaction.Ammount,
                isReconciled: transaction.Reconciled
            );
        }

        public static AccountResponse MapToResponse(this BankAccount account)
        {
            return new AccountResponse(account.AccountId, account.BanckId);
        }

        public static FileResponse MapToResponse(this ImportedFile file)
        {
            return new FileResponse(file.Id,file.FileName,file.ImportDate,file.BankAccount.MapToResponse());
        }
    }
}