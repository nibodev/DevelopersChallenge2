using DevelopersChallenge2.Domain;
using DevelopersChallenge2.Repository.Interfaces;
using DevelopersChallenge2.Service.Interfaces;
using DevelopersChallenge2.Service.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopersChallenge2.Service.Servicies
{
    public class BankListService : BaseService<BANKTRANLIST>, IBankListService
    {
        private readonly OFXRegex OfxRegex;

        private readonly IBankListRepository repository;
        public BankListService(IBankListRepository repository) : base(repository) => this.repository = repository;

        public async Task<BankList> PostBankList(Stream requestBody)
        {
            var bankTranactionList = await ParseOFX(requestBody);
            var bankList = await ConvertToBankList(bankTranactionList);
            return bankList;
        }

        public async Task<BankList> ConvertToBankList(List<BANKTRANLIST> bankTranList)
        {
            // Declared to test date
            DateTime dateStart = new DateTime();
            DateTime dateEnd = new DateTime();

            // Create an empty HashSet to fill later
            HashSet<Transaction> transactions = new HashSet<Transaction>();

            foreach (var bankItem in bankTranList)
            {
                // Get the dates to test
                var dateStartTemp = ConvertToDateTime(bankItem.DTSTART);
                var dateEndTemp = ConvertToDateTime(bankItem.DTEND);

                foreach (var transaction in bankItem.STMTTRN)
                {
                    // Get all no repeated transaction
                    transactions.Add(new Transaction()
                    {
                        DatePosted = ConvertToDateTime(transaction.DTPOSTED),
                        Memo = transaction.MEMO,
                        TransactionType = transaction.TRNTYPE,
                        TransactionAmount = Convert.ToDecimal(transaction.TRNAMT)
                    });
                }

                // Get greater date in start and end
                if (dateStartTemp > dateStart)
                    dateStart = dateStartTemp;

                if (dateEndTemp > dateEnd)
                    dateEnd = dateEndTemp;
            }

            // Return BankList
            return new BankList()
            {
                DateStart = dateStart,
                DateEnd = dateEnd,
                Transactions = transactions.ToList()
            };
        }

        private DateTime ConvertToDateTime(string date)
        {
            string start = OfxRegex.OnlySixNumbers.Match(date).ToString();
            int year = Convert.ToInt32(start.Substring(0, 4));
            int mounth = Convert.ToInt32(start.Substring(4, 2));
            int day = Convert.ToInt32(start.Substring(6, 2));
            
            return new DateTime(year, mounth, day);
        }

        public async Task<List<BANKTRANLIST>> ParseOFX(Stream requestBody)
        {
            List<BANKTRANLIST> result = new List<BANKTRANLIST>();
            
            using (var stream = new StreamReader(requestBody))
                result = OFXParserUtil.Parser(stream.ReadToEnd());
            
            return result;
        }
    }
}
