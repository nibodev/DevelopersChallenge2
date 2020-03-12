using DevelopersChallenge2.Domain;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DevelopersChallenge2.Service.Util
{
    public abstract class OFXParserUtil
    {
        private static readonly OFXRegex OfxRegex;

        public static List<BANKTRANLIST> Parser(string docFile)
        {
            // Extract only OFX scheme
            string[] stringFiles = ExtractOFX(docFile);

            List<BANKTRANLIST> bANKTRANLIST = new List<BANKTRANLIST>();
            
            // A loop for every OFX File getting only necessary informations
            for (var i = 0; i < stringFiles.Length; i++)
            {
                bANKTRANLIST.Add(new BANKTRANLIST()
                {
                    DTSTART = OfxRegex.OFXAfterTagDTSTART.Match(stringFiles[i]).Value.Replace("<DTSTART>", ""),
                    DTEND = OfxRegex.OFXAfterTagDTEND.Match(stringFiles[i]).Value.Replace("<DTEND>", ""),
                });

                // How STMTRN is another list inside BANKTRANLIST, I've take more one loop here
                var sTMTRN = OfxRegex.OFXBetweenTagSTMTTRN.Matches(stringFiles[i]);
                for (var j = 0; j < sTMTRN.Count; j++)
                {
                    bANKTRANLIST[i].STMTTRN.Add(new STMTTRN()
                    {
                        TRNTYPE = OfxRegex.OFXAfterTagTRNTYPE.Match(sTMTRN[j].Value).Value.Replace("<TRNTYPE>", ""),
                        TRNAMT = OfxRegex.OFXAfterTagTRNAMT.Match(sTMTRN[j].Value).Value.Replace("<TRNAMT>", ""),
                        DTPOSTED = OfxRegex.OFXAfterTagDTPOSTED.Match(sTMTRN[j].Value).Value.Replace("<DTPOSTED>", ""),
                        MEMO = OfxRegex.OFXAfterTagMEMO.Match(sTMTRN[j].Value).Value.Replace("<MEMO>", "")
                    });
                }
            }
            
            return bANKTRANLIST;
        }

        public static string[] ExtractOFX(string docFile)
        {
            MatchCollection files = OfxRegex.OFXBetweenTagOFX.Matches(docFile);
            string[] stringFiles = new string[files.Count];

            for (var i = 0; i < files.Count; i++)
                stringFiles[i] += files[i].Value;

            return stringFiles;
        }
    }
}
