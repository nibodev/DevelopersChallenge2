using DevelopersChallenge2.Domain;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Service.Interfaces
{
    public interface IBankListService : IBaseService<BankList>
    {
        // Parse OFX file to BANKTRANLIST class list
        Task<List<BANKTRANLIST>> ParseOFX(Stream requestBody);

        // Converts to a friendely class
        Task<BankList> ConvertToBankList(List<BANKTRANLIST> bankTranList);
        
        // Save bank list with transactions
        Task<BankList> PostBankList(Stream requestBody);
    }
}
