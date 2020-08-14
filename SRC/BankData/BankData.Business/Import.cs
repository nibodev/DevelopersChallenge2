using System.Linq;
using System.Xml.Linq;
using System;
using System.IO;
using System.Data;
using System.Globalization;

namespace BankData.Business
{
    public class Import
    {
        private static string bankId;
        public static XElement ReadFile(string pathToOfxFile)
        {
            //bank
            var bank = from line in File.ReadAllLines(pathToOfxFile)
                       where line.Contains("<BANKID>")
                       select line;
            bankId = GetTagValue(bank.ToList()[0]);

            //transactions
            var tags = from line in File.ReadAllLines(pathToOfxFile)
                       where line.Contains("<STMTTRN>") ||
                       line.Contains("<TRNTYPE>") ||
                       line.Contains("<DTPOSTED>") ||
                       line.Contains("<TRNAMT>") ||
                       line.Contains("<MEMO>")
                       select line;

            XElement el = new XElement("root");
            XElement son = null;
            foreach (var l in tags)
            {
                if (l.IndexOf("<STMTTRN>") != -1)
                {
                    son = new XElement("STMTTRN");
                    el.Add(son);
                    continue;
                }

                var tagName = GetTagName(l);
                var elSon = new XElement(tagName);
                elSon.Value = GetTagValue(l);
                if (l.IndexOf("<DTPOSTED>") != -1)
                {
                    elSon.Value = DateTime.ParseExact(GetTagValue(l), "yyyyMMdd", null).ToString();
                }
                son.Add(elSon);
            }
            return el;
        }
        /// <summary>
        /// Get the text of the tag
        /// </summary>
        /// <param name="line">Line of file</param>
        /// <returns>Tag name</returns>
        private static string GetTagName(string line)
        {
            int pos_init = line.IndexOf("<") + 1;
            int pos_end = line.IndexOf(">");
            pos_end = pos_end - pos_init;
            return line.Substring(pos_init, pos_end);
        }
        /// <summary>
        /// Get the value of the tag
        /// </summary>
        /// <param name="line">Line of file</param>
        /// <returns>Tag value</returns>
        private static string GetTagValue(string line)
        {
            int pos_init = line.IndexOf(">") + 1;
            string retValue = line.Substring(pos_init).Trim();
            if (retValue.IndexOf("[") != -1)
            {
                // DatePosted with 8 digits
                retValue = retValue.Substring(0, 8);
            }
            return retValue;
        }
        /// <summary>
        /// Import OFX file
        /// </summary>
        /// <param name="pathToOfxFile">Complete path of file</param>
        /// <returns>True if succeeded (import and save data)</returns>
        public static bool ImportFile(string pathToOfxFile)
        {
            bool booIsOK = true;
            DataSet ds = new DataSet();
            DateTime dateUpload = System.DateTime.Now;

            if (!System.IO.File.Exists(pathToOfxFile))
            {
                throw new Exception("File not found!");
            }

            XElement doc = ReadFile(pathToOfxFile);
            string xml = doc.ToString();
            ds.ReadXml(new System.IO.StringReader(xml));

            try
            {
                // insert rows
                Transaction trans = new Transaction();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    trans.BankId = bankId;
                    trans.Type = row["TRNTYPE"].ToString();
                    trans.DatePosted = Convert.ToDateTime(row["DTPOSTED"].ToString());
                    trans.DateUpload = dateUpload;
                    trans.Memo = row["MEMO"].ToString();
                    trans.Amount = row["TRNAMT"].ToString();

                    // insert only if transaction was not uploaded before (not duplicated)
                    String dateUploadDB = trans.CheckDuplicated();
                    if ((string.IsNullOrEmpty(dateUploadDB)) || (Convert.ToString(dateUpload) == dateUploadDB)){
                        trans.Insert();
                    }
                }
                trans = null;
            }
            catch
            {
                booIsOK = false;
            }

            return booIsOK;
        }
    }
}
