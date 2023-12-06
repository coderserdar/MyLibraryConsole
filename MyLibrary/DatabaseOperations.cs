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
            Console.WriteLine("Connected to SQLite!\n");
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

        Console.WriteLine("|ID\t|Book Name\t\t\t|Book Category\t\t|Book Writer\t\t\t|Book Description\t\t|Is Book Active\t\t|Is Book Read");
        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            int bookId = (int)Convert.ToInt64(sqlite_datareader["Id"]);
            string bookName = (string)sqlite_datareader["BookName"];
            string bookCategory = (string)sqlite_datareader["BookCategory"];
            string writer = (string)sqlite_datareader["Writer"];
            string bookDescription = sqlite_datareader["BookDescription"] != null ? "" : (string)sqlite_datareader["BookDescription"];
            int activeStatus = sqlite_datareader["ActiveStatus"] != null ? 0 : (int)Convert.ToInt64(sqlite_datareader["ActiveStatus"]);
            int readUnread = (int)Convert.ToInt64(sqlite_datareader["ReadUnread"]);

            Console.WriteLine($"|{bookId,-7}" + $"|{LimitLength(bookName, 25),-31}" + $"|{LimitLength(bookCategory, 18),-23}" +
            $"|{LimitLength(writer, 26),-31}" + $"|{LimitLength(bookDescription, 16),-31}" + $"|{activeStatus,-23}" + $"|{readUnread,-5}");
        }
    }

    public void InsertData(Book newBook)
    {
        string insertSql = "INSERT INTO Book (BookName, BookCategory, Writer, BookDescription, ReadUnread) VALUES (@BookName, @BookCategory, @Writer, @BookDescription, @ReadUnread)";
        SqliteCommand insertCommand = new SqliteCommand(insertSql, sqlite_conn);
        insertCommand.Parameters.AddWithValue("@BookName", newBook.BookName);
        insertCommand.Parameters.AddWithValue("@BookCategory", newBook.BookCategory);
        insertCommand.Parameters.AddWithValue("@Writer", newBook.Writer);
        insertCommand.Parameters.AddWithValue("@BookDescription", newBook.BookDescription);
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

    public static string LimitLength(string source, int maxLength)
    {
        if (source.Length <= maxLength)
        {
            return source;
        }
        return source.Substring(0, maxLength);
    }
}