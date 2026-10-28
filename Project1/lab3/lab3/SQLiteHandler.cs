using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace lab3
{
    /// <summary>
    /// Acces la baza de date SQLite (ADO.NET) pentru cuvintele cheie.
    /// </summary>
    public class SQLiteHandler : IDisposable
    {
        private static readonly string[] DefaultKeywords =
            { "facebook", "youtube", "instagram", "twitter", "reddit", "tiktok" };

        private readonly string connectionString =
            "Data Source=" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keywords.db");

        private SqliteConnection connection;

        public bool IsConnected
        {
            get { return connection != null && connection.State == ConnectionState.Open; }
        }

        public void ConnectToDb()
        {
            if (IsConnected)
                return;

            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }

            connection = new SqliteConnection(connectionString);
            connection.Open();

            bool firstRun = !TableExists("Keywords");

            Execute(
                "CREATE TABLE IF NOT EXISTS Keywords (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Keyword TEXT NOT NULL);");

            // Baze de date mai vechi: eliminam dublurile, apoi impunem unicitatea.
            Execute(
                "DELETE FROM Keywords WHERE Id NOT IN " +
                "(SELECT MIN(Id) FROM Keywords GROUP BY Keyword COLLATE NOCASE);");
            Execute(
                "CREATE UNIQUE INDEX IF NOT EXISTS IX_Keywords_Keyword " +
                "ON Keywords (Keyword COLLATE NOCASE);");

            // La prima pornire se adauga lista initiala de cuvinte blocate.
            if (firstRun)
            {
                foreach (string keyword in DefaultKeywords)
                    InsertKeyword(keyword);
            }
        }

        public void DisconnectFromDb()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public bool KeywordExists(string k)
        {
            EnsureConnected();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT COUNT(*) FROM Keywords WHERE Keyword = $keyword COLLATE NOCASE;";
                command.Parameters.AddWithValue("$keyword", (k ?? string.Empty).Trim());
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public void InsertKeyword(string k)
        {
            EnsureConnected();

            if (string.IsNullOrWhiteSpace(k))
                throw new ArgumentException("The keyword cannot be empty.", "k");

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Keywords (Keyword) VALUES ($keyword);";
                command.Parameters.AddWithValue("$keyword", k.Trim());
                command.ExecuteNonQuery();
            }
        }

        public bool DeleteKeyword(string k)
        {
            EnsureConnected();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Keywords WHERE Keyword = $keyword COLLATE NOCASE;";
                command.Parameters.AddWithValue("$keyword", (k ?? string.Empty).Trim());
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<string> GetAllKeywords()
        {
            EnsureConnected();

            List<string> keywords = new List<string>();
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Keyword FROM Keywords ORDER BY Keyword COLLATE NOCASE;";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        keywords.Add(reader.GetString(0));
                }
            }

            return keywords;
        }

        private bool TableExists(string tableName)
        {
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = $name;";
                command.Parameters.AddWithValue("$name", tableName);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private void Execute(string sql)
        {
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void EnsureConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException("The database is not connected.");
        }

        public void Dispose()
        {
            DisconnectFromDb();
        }
    }
}
