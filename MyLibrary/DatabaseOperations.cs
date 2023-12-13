using Microsoft.Data.Sqlite;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

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

        string dashes = string.Concat(Enumerable.Repeat("-", 166));
        Console.WriteLine(dashes);
        string top = "|ID\t|Book Name\t\t\t|Book Category\t\t|Book Writer\t\t\t|Book Description\t\t|Is Book Active\t\t|Is Book Read|";
        Console.WriteLine(top);
        Console.WriteLine(dashes);

        sqlite_datareader = sqlite_cmd.ExecuteReader();
        while (sqlite_datareader.Read())
        {
            int bookId = (int)Convert.ToInt64(sqlite_datareader["Id"]);
            string bookName = (string)sqlite_datareader["BookName"];
            string bookCategory = (string)sqlite_datareader["BookCategory"];
            string writer = (string)sqlite_datareader["Writer"];
            string bookDescription = sqlite_datareader["BookDescription"].GetType() == typeof(DBNull) ? "" : (string)sqlite_datareader["BookDescription"];
            int activeStatus = sqlite_datareader["ActiveStatus"].GetType() == typeof(DBNull) ? 0 : (int)Convert.ToInt64(sqlite_datareader["ActiveStatus"]);
            int readUnread = (int)Convert.ToInt64(sqlite_datareader["ReadUnread"]);

            Console.WriteLine($"|{bookId,-7}" + $"|{LimitLength(bookName, 25),-31}" + $"|{LimitLength(bookCategory, 18),-23}" +
            $"|{LimitLength(writer, 26),-31}" + $"|{LimitLength(bookDescription, 16),-31}" + $"|{activeStatus,-23}" + $"|{readUnread,-12}|");
            Console.WriteLine(dashes);
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

    public void UpdateData(int selectedId, string columnName, string updatedData)
    {
        string updateSql = $"UPDATE Book SET {columnName} = '{updatedData}' WHERE Id = {selectedId}";
        SqliteCommand updateCommand = new SqliteCommand(updateSql, sqlite_conn);

        try
        {
            updateCommand.ExecuteNonQuery();
            Console.WriteLine("Data successfully updated.");
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

    public string CheckIdAvailable(int selecId)
    {
        string controlIdSql = $"SELECT EXISTS (SELECT 1 FROM Book WHERE Id = {selecId})";
        SqliteCommand controlIdCommand = new SqliteCommand(controlIdSql, sqlite_conn);
        return controlIdCommand.ExecuteScalar().ToString();
    }

    public bool CheckNameWriterAvailable(string isName, string writerName)
    {
        SqliteCommand controlNameCommand = new("SELECT BookName, Writer from Book", sqlite_conn);
        SqliteDataReader reader = controlNameCommand.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (isName.ToLower() == reader[0].ToString().ToLower())
                {
                    if (writerName.ToLower() == reader[1].ToString().ToLower())
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No rows found.");
        }

        reader.Close();
        return true;
    }
}