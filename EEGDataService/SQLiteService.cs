using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EEGDataService.Models;

namespace EEGDataService
{
    public  class SQLiteService
    {

        private static SQLiteConnection sqLiteConnection;
        public static string DbFile => Environment.CurrentDirectory + @"\EEGDb.sqlite";


        public SQLiteService()
        {
            sqLiteConnection = new SQLiteConnection($"Data Source={DbFile}");
            if (!File.Exists(DbFile))
            {
                CreateDb();
                CreateTable();
            }
          
        }

        public void CreateDb()
        {
            SQLiteConnection.CreateFile("sqlite.db");
        }

        public void CreateTable()
        {
            sqLiteConnection.Open();
            sqLiteConnection.Execute(@"CREATE TABLE `EEGData` (
                `RawData`	INTEGER NOT NULL,
                `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                `Time`	DATETIME NOT NULL );");
            sqLiteConnection.Close();
        }


        public async Task<IEnumerable<EEGData>> GetEEGData()
        {
            sqLiteConnection.Open();
            IEnumerable<EEGData> result = await sqLiteConnection.QueryAsync<EEGData>(@"SELECT * FROM EEGData");
            sqLiteConnection.Close();
            return result;
        }

        public async Task WriteEEGData(IEnumerable<EEGData> eegDataList)
        {
            sqLiteConnection.Open();
            ExecuteNonQuery(sqLiteConnection, "INSERT INTO EEGData (RawData, Time) VALUES (@RawData, @Time);", eegDataList);                     
            sqLiteConnection.Close();
        }

        private string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        }

        public static void ExecuteNonQuery(
            SQLiteConnection sqLiteConnection,
            string commandText,
            object param = null)
        {
            // Ensure we have a connection
            if (sqLiteConnection == null)
            {
                throw new NullReferenceException(
                    "Please provide a connection");
            }

            // Ensure that the connection state is Open
            if (sqLiteConnection.State != ConnectionState.Open)
            {
                sqLiteConnection.Open();
            }

            // Use Dapper to execute the given query
            sqLiteConnection.Execute(commandText, param);
        }
    }
}
