using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            CreateTable();
        }

        public void CreateDb()
        {
            SQLiteConnection.CreateFile("sqlite.db");
        }

        public void CreateTable()
        {
            
            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = new SQLiteCommand(@"CREATE TABLE `EEGData` (
                `RawData`	INTEGER NOT NULL,
                `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                `Time`	DATETIME NOT NULL );");
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }


        public async Task<IEnumerable<EEGData>> GetEEGData()
        {
            sqLiteConnection.Open();
            IEnumerable<EEGData> result =  sqLiteConnection.Query<EEGData>(@"SELECT * FROM EGGData");
            sqLiteConnection.Close();
            return result;
        }
    }
}
