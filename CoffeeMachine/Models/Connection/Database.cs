using System;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls.WebParts;

namespace CoffeeMachine.Models.Connection
{
    public class Database : IDisposable
    {
        public SQLiteConnection Connection;

        public Database()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.
                GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(path).Value;
            string connectionString = "Data Source=" + appRoot + "/sqlite3.db";
            Connection = new SQLiteConnection(connectionString);
            if (!File.Exists("./sqlite3.db")) return;
            SQLiteConnection.CreateFile("sqlite3.db");
            Console.WriteLine("Database file created");
        }

        public void OpenConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();

            }
        }

        public void CloseConnection()
        {
            if (Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
            }
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}