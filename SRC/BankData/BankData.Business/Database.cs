using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Web;

namespace BankData.Business
{
    class Database
    {
        public System.Data.SQLite.SQLiteConnection conn { get; set; }

        /// <summary>
        /// Constructor - connect to database
        /// </summary>
        /// <returns></returns>
        public Database() 
        { 
            OpenDB();
        }
        /// <summary>
        /// Desctructor
        /// </summary>
        /// <returns></returns>
        ~Database()
        {
            CloseDB();
        }
        private void OpenDB()
        {
            string dbFile = System.AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Database.db";
            conn = new System.Data.SQLite.SQLiteConnection("Data Source=" + dbFile);
            conn.Open();
        }
        private void CloseDB()
        {
            conn.Close();
        }

    }
}
