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
        Console.WriteLine("|ID\t|Book Name\t|Book Category\t|Book Writer\t\t|Book Description\t|Is Book Active\t\t|Is Book Read");
        while (sqlite_datareader.Read())
        {
            Console.WriteLine("|" + sqlite_datareader["Id"] +
            "\t|" + sqlite_datareader["BookName"] +
            "\t\t|" + sqlite_datareader["BookCategory"] +
            "\t\t|" + sqlite_datareader["Writer"] +
            "\t\t\t|" + sqlite_datareader["BookDescription"] +
            "\t\t\t|" + sqlite_datareader["ActiveStatus"] +
            "\t\t\t|" + sqlite_datareader["ReadUnread"]);
        }
    }

    public void InsertData(Book newBook)
    {
        string insertSql = "INSERT INTO Book (BookName, BookCategory, Writer, ReadUnread) VALUES (@BookName, @BookCategory, @Writer, @ReadUnread)";
        SqliteCommand insertCommand = new SqliteCommand(insertSql, sqlite_conn);
        insertCommand.Parameters.AddWithValue("@BookName", newBook.BookName);
        insertCommand.Parameters.AddWithValue("@BookCategory", newBook.BookCategory);
        insertCommand.Parameters.AddWithValue("@Writer", newBook.Writer);
        insertCommand.Parameters.AddWithValue("@ReadUnread", newBook.ReadUnread);
        try
        {
            insertCommand.ExecuteNonQuery();
            Console.WriteLine("Data inserted.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void DeleteRow(int idNumber)
    {
        string deleteSql = $"DELETE FROM Book WHERE Id = {idNumber}";
        SqliteCommand deleteCommand = new SqliteCommand(deleteSql, sqlite_conn);
        try
        {
            deleteCommand.ExecuteNonQuery();
            Console.WriteLine($"{idNumber}. id has been deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public void CloseConnection()
    {
        sqlite_conn.Close();
        Console.WriteLine("Connection has been closed.");
    }

}