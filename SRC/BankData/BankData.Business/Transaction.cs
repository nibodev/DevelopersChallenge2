using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BankData.Business
{
    public class Transaction
    {
        public string BankId { get; set; }

        public string Type { get; set; }

        public DateTime DatePosted { get; set; }

        public string Amount { get; set; }

        public string Memo { get; set; }

        public DateTime DateUpload{ get; set; }

        private Database db = null;

        public Transaction()
        {
            db = new Database();
        }
        ~Transaction()
        {
            db = null;
        }

        /// <summary>
        /// Insert transaction
        /// </summary>
        /// <returns></returns>
        public void Insert()
        {
            var command = new System.Data.SQLite.SQLiteCommand(db.conn);
            string qry = "insert into 'Transaction' (BankId, Type, DatePosted, DateUpload, Memo, Amount) " +
                " VALUES ('" + BankId + "', '" + Type + "', '" + DatePosted + "', '" + DateUpload + "', '" + Memo + "', '" + Amount + "')";
            command.CommandText = qry;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// List transactions
        /// </summary>
        /// <param name="sortText">string used to sort the results</param>
        /// <returns>List data</returns>
        public DataTable List(string sortText)
        {
            var command = new System.Data.SQLite.SQLiteCommand(db.conn);
            if (string.IsNullOrEmpty(sortText))
            {
                sortText = " BankId, DatePosted ";
            }
            string qry = "select id, BankId, Type, DatePosted, DateUpload, Memo, Amount from 'Transaction' order by " + sortText;
            command.CommandText = qry;
            DataTable dt = new DataTable();
            System.Data.SQLite.SQLiteDataReader dr = command.ExecuteReader();
            dt.Load(dr);
            return dt;
        }
        /// <summary>
        /// List transactions by date
        /// </summary>
        /// <param name="startDate">start date to search</param>
        /// <param name="endDate">end date to search</param>
        /// <param name="sortText">string used to sort the results</param>
        /// <returns>List transactions data by range of dates</returns>
        public DataTable List(string startDate, string endDate, string sortText)
        {
            var command = new System.Data.SQLite.SQLiteCommand(db.conn);
            if (string.IsNullOrEmpty(sortText))
            {
                sortText = " BankId, DatePosted ";
            }

            startDate = startDate.Substring(6) + startDate.Substring(3, 2) + startDate.Substring(0, 2);
            endDate = endDate.Substring(6) + endDate.Substring(3, 2) + endDate.Substring(0, 2);

            string qry = "select id, BankId, Type, DatePosted, DateUpload, Memo, Amount from 'Transaction' " +
                " where substr(DatePosted,7,4)||substr(DatePosted,4,2)||substr(DatePosted,1,2) " +
                " between '"+ startDate +"' and '"+ endDate +"' " +
                " order by " + sortText;
            command.CommandText = qry;
            DataTable dt = new DataTable();
            System.Data.SQLite.SQLiteDataReader dr = command.ExecuteReader();
            dt.Load(dr);
            return dt;
        }
        /// <summary>
        /// Check duplicate data
        /// </summary>
        /// <returns>If data already exists, returns the date when the file was uploaded</returns>
        public string CheckDuplicated()
        {
            string DateUpload = string.Empty;
            var command = new System.Data.SQLite.SQLiteCommand(db.conn);
            string qry = "select DateUpload from 'Transaction' " +
                " where BankId = '" + BankId + "' " +
                " and Type = '" + Type + "' " +
                " and DatePosted = '" + DatePosted + "' " +
                " and Amount = '" + Amount + "' " +
                " and Memo = '" + Memo + "' ";
            
            command.CommandText = qry;
            DataTable dt = new DataTable();
            System.Data.SQLite.SQLiteDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                DateUpload = dr["DateUpload"].ToString();
            }
            return DateUpload;
        }
    }
}
