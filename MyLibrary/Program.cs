using System;
using System.Linq;
using Microsoft.Data.Sqlite;


DatabaseOperations our_database = new();
int status = 5;

Console.WriteLine("Welcome to the App");

Console.WriteLine("Possible Actions:\n" +
    "List all data from database: 1\n" +
    "Insert data to database: 2\n" +
    "Update data from database: 3\n" +
    "Delete data from database: 4");

while (status != 0)
{
    Console.WriteLine("Please enter preferred action. 0 for exit.");
    status = Convert.ToInt32(Console.ReadLine());

    switch (status)
    {
        case 0:
            our_database.CloseConnection();
            break;
        case 1:
            our_database.ReadData();
            continue;
        case 2:
            Console.WriteLine("Please enter name of the new book.");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string bookName = Console.ReadLine();
            Console.WriteLine("Please enter category of the new book.");
            string bookCategory = Console.ReadLine();
            Console.WriteLine("Please enter writers name.");
            string writer = Console.ReadLine();

            Console.WriteLine("Has book been read?");
            string answer = Console.ReadLine();
            int isBookRead;
            if (answer == "yes")
            {
                isBookRead = 1;
            }
            else
            {
                isBookRead = 0;
            }
            Book newBook = new Book
            {
                BookName = bookName,
                BookCategory = bookCategory,
                Writer = writer,
                ReadUnread = isBookRead
            };

            Console.WriteLine("Do you want to add description for the book?");
            answer = Console.ReadLine();
            if (answer == "yes")
            {
                Console.WriteLine("Please enter description.");
                string bookDescription = Console.ReadLine();
                newBook.BookDescription = bookDescription;
            }
            our_database.InsertData(newBook);
            continue;
        case 3:
            continue;
        case 4:
            Console.WriteLine("Please enter the id that you want to delete.");
            int prefferedId = Convert.ToInt32(Console.ReadLine());
            our_database.DeleteRow(prefferedId);
            continue;
    }
}
