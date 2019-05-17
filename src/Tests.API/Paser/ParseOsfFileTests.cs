using System;
using System.IO;
using System.Linq;
using API.Domain;
using API.Services;
using Xunit;

namespace Tests.API.Paser
{
    public class ParseOsfFileTests
    {
        private readonly ImportedFile _importedFile;

        #region OFX data
        const string Ofx = @"
OFXHEADER:100
DATA:OFXSGML
VERSION:102
SECURITY:NONE
ENCODING:USASCII
CHARSET:1252
COMPRESSION:NONE
OLDFILEUID:NONE
NEWFILEUID:NONE

<OFX>
<SIGNONMSGSRSV1>
<SONRS>
<STATUS>
<CODE>0
<SEVERITY>INFO
</STATUS>
<DTSERVER>20140318100000[-03:EST]
<LANGUAGE>POR
</SONRS>
</SIGNONMSGSRSV1>
<BANKMSGSRSV1>
<STMTTRNRS>
<TRNUID>1001
<STATUS>
<CODE>0
<SEVERITY>INFO
</STATUS>
<STMTRS>
<CURDEF>BRL
<BANKACCTFROM>
<BANKID>0341
<ACCTID>7037300576
<ACCTTYPE>CHECKING
</BANKACCTFROM>
<BANKTRANLIST>
<DTSTART>20140201100000[-03:EST]
<DTEND>2014020100000[-03:EST]
<STMTTRN>
<TRNTYPE>DEBIT
<DTPOSTED>20140203100000[-03:EST]
<TRNAMT>-140.00
<MEMO>CXE     001958 SAQUE    
</STMTTRN>
<STMTTRN>
<TRNTYPE>DEBIT
<DTPOSTED>20140204100000[-03:EST]
<TRNAMT>-102.19
<MEMO>RSHOP-SUPERMERCAD-03/02 
</STMTTRN>
<STMTTRN>
<TRNTYPE>DEBIT
<DTPOSTED>20140204100000[-03:EST]
<TRNAMT>-4000.00
<MEMO>TBI 0304.40719-0     C/C
</STMTTRN>
</BANKTRANLIST>
<LEDGERBAL>
<BALAMT>-4021.44
<DTASOF>20140318100000[-03:EST]
</LEDGERBAL>
</STMTRS>
</STMTTRNRS>
</BANKMSGSRSV1>
</OFX>";
        #endregion

        public ParseOsfFileTests()
        {
            _importedFile = new ImportedFile("teste", StreamReader.Null).UpdateContent(Ofx);

            new OsfParser(_importedFile).Parse();
        }

        [Fact]
        public void Should_parse_transactions_and_bankaccont()
        {
            Assert.Equal(3,_importedFile.Transactions.Count());
            Assert.NotNull(_importedFile.BankAccount);
        }

        [Fact]
        public void The_account_shoul_have_the_correct_data()
        {
            Assert.Equal("0341",_importedFile.BankAccount.BanckId);
            Assert.Equal("7037300576",_importedFile.BankAccount.AccountId);
            Assert.Equal(AccountType.Checking, _importedFile.BankAccount.Type);
        }

        [Fact]
        public void The_transaction_details_shoul_be_mapped()
        {
            var transaction = _importedFile.Transactions.Single(x => x.Description.Contains("SAQUE"));

            Assert.Equal("CXE     001958 SAQUE    ", transaction.Description);
            Assert.Equal(TransactionType.Debit,transaction.Type);
            Assert.Equal(140.00m, transaction.Ammount);
            Assert.Equal(DateTime.Parse("2014-01-31T05:00:00"),transaction.Date);
            Assert.False(transaction.Reconciled);
            Assert.Equal(_importedFile,transaction.File);
        }
    }
}
