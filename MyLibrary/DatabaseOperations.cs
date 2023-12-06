using Microsoft.Data.Sqlite;
using System;

public class DatabaseOperations
{
    public SqliteConnection sqlite_conn = new("Data Source=Data/Book.db;");

    public DatabaseOperations()
    {
        try
        {
            sqlite_conn.Open();
            Console.WriteLine("Connected to SQLite!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ReadData()
    {
        SqliteDataReader sqlite_datareader;
        SqliteCommand sqlite_cmd;
        sqlite_cmd = sqlite_conn.CreateCommand();
        sqlite_cmd.CommandText = "SELECT * FROM Book";

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            Console.WriteLine("ID: \t\t\t" + sqlite_datareader["Id"] +
            "\nBook Name: \t\t" + sqlite_datareader["BookName"] +
            "\nBook Category: \t\t" + sqlite_datareader["BookCategory"] +
            "\nBook Writer: \t\t" + sqlite_datareader["Writer"] +
            "\nBook Description: \t" + sqlite_datareader["BookDescription"] +
            "\nIs Book Active: \t" + sqlite_datareader["ActiveStatus"] +
            "\nIs Book Read: \t\t" + sqlite_datareader["ReadUnread"]);
        }
        sqlite_conn.Close();
    }

}