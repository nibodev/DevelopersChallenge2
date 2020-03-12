using System.Text.RegularExpressions;

namespace DevelopersChallenge2.Service.Util
{
    public class OFXRegex
    {
        public Regex OFXBetweenTagOFX { get; set; } = new Regex(@"<OFX>(.|\n)*?<\/OFX>", RegexOptions.IgnoreCase);

        public Regex OFXBetweenTagSONRS { get; set; } = new Regex(@"<SONRS>(.|\n)*?<\/SONRS>", RegexOptions.IgnoreCase);
        public Regex OFXBetweenTagSTATUS { get; set; } = new Regex(@"<STATUS>(.|\n)*?<\/STATUS>", RegexOptions.IgnoreCase);

        public Regex OFXBetweenTagBANKTRANLIST { get; set; } = new Regex(@"<BANKTRANLIST>(.|\n)*?<\/BANKTRANLIST>", RegexOptions.IgnoreCase);
        public Regex OFXBetweenTagSTMTTRN { get; set; } = new Regex(@"<STMTTRN>(.|\n)*?<\/STMTTRN>", RegexOptions.IgnoreCase);

        public Regex OFXAfterTagDTSTART { get; set; } = new Regex(@"(?:<DTSTART>)(.*)", RegexOptions.IgnoreCase);
        public Regex OFXAfterTagDTEND { get; set; } = new Regex(@"(?:<DTEND>)(.*)", RegexOptions.IgnoreCase);

        public Regex OFXAfterTagTRNTYPE { get; set; } = new Regex(@"(?:<TRNTYPE>)(.*)", RegexOptions.IgnoreCase);
        public Regex OFXAfterTagDTPOSTED { get; set; } = new Regex(@"(?:<DTPOSTED>)(.*)", RegexOptions.IgnoreCase);
        public Regex OFXAfterTagTRNAMT { get; set; } = new Regex(@"(?:<TRNAMT>)(.*)", RegexOptions.IgnoreCase);
        public Regex OFXAfterTagMEMO { get; set; } = new Regex(@"(?:<MEMO>)(.*)", RegexOptions.IgnoreCase);


        public Regex OnlyNumbers { get; set; } = new Regex(@"(\d+)");

        public Regex OnlySixNumbers { get; set; } = new Regex(@"\d{8}");
    }
}
