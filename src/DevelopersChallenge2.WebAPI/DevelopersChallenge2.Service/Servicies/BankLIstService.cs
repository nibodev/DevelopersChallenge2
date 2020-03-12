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
            try
            {
                // Declared to test date
                DateTime dateStart = new DateTime();
                DateTime dateEnd = new DateTime();

                // Create an empty list to fill later
                List<Transaction> transactionsTemp = new List<Transaction>();

                // List no repeat itens
                List<Transaction> transactions = new List<Transaction>();

                foreach (var bankItem in bankTranList)
                {
                    // Get the dates to test
                    var dateStartTemp = ConvertToDateTime(bankItem.DTSTART);
                    var dateEndTemp = ConvertToDateTime(bankItem.DTEND);

                    foreach (var transaction in bankItem.STMTTRN)
                    {
                        // Get all
                        var tran = new Transaction()
                        {
                            DatePosted = ConvertToDateTime(transaction.DTPOSTED),
                            Memo = transaction.MEMO,
                            TransactionType = transaction.TRNTYPE,
                            TransactionAmount = Convert.ToDecimal(transaction.TRNAMT)
                        };
                        transactionsTemp.Add(tran);
                    }

                    // Get greater date in start and end
                    if (dateStartTemp > dateStart)
                        dateStart = dateStartTemp;

                    if (dateEndTemp > dateEnd)
                        dateEnd = dateEndTemp;
                }

                transactions = RemoveDuplicates(transactionsTemp);

                // Return BankList
                return new BankList()
                {
                    DateStart = dateStart,
                    DateEnd = dateEnd,
                    Transactions = transactions.ToList()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Transaction> RemoveDuplicates(List<Transaction> transactionsTemp)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                foreach (var tranTemp in transactionsTemp)
                {
                    if (transactions.Count < 1)
                    {
                        transactions.Add(tranTemp);
                    }
                    else
                    {
                        transactions = AddNoRepeatedItems(transactions, tranTemp);
                    }
                }

                return transactions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Transaction> AddNoRepeatedItems(List<Transaction> transactions, Transaction tranTemp)
        {
            List<Transaction> listTransactions = transactions;

            foreach (var tran in transactions)
            {
                if (DateTime.Compare(tran.DatePosted, tranTemp.DatePosted) == 0 &&
                    tran.TransactionType == tranTemp.TransactionType &&
                    tran.TransactionAmount == tranTemp.TransactionAmount &&
                    tran.Memo == tranTemp.Memo)
                {
                    return transactions;
                }
            }

            listTransactions.Add(tranTemp);
            
            return listTransactions;
        }

        private DateTime ConvertToDateTime(string date)
        {
            OFXRegex OfxRegex = new OFXRegex();
            try
            {
                string start = OfxRegex.OnlySixNumbers.Match(date).ToString();
                int year = Convert.ToInt32(start.Substring(0, 4));
                int mounth = Convert.ToInt32(start.Substring(4, 2));
                int day = Convert.ToInt32(start.Substring(6, 2));

                if (DateTime.IsLeapYear(year))
                {
                    if (day > 29 && mounth == 2) { day = 29; }
                }
                else
                {
                    if (day > 28 && mounth == 2) { day = 28; }
                }

                return new DateTime(year, mounth, day);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
