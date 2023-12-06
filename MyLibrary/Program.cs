using System;
using System.Linq;
using Microsoft.Data.Sqlite;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.


DatabaseOperations our_database = new();
int status = 6;

Console.WriteLine("Welcome to the App\n");

Console.WriteLine("Possible Actions:\n" +
    "List all data from database: 1\n" +
    "Insert data to database: 2\n" +
    "Update data from database: 3\n" +
    "Delete data from database: 4\n");

while (status != 0)
{
    Console.WriteLine("Please enter preferred action. 0 for exit. 5 for help.");
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
            string bookName = Console.ReadLine();
            Console.WriteLine("Please enter category of the new book.");
            string bookCategory = Console.ReadLine();
            Console.WriteLine("Please enter writers name.");
            string writer = Console.ReadLine();
            Console.WriteLine("Has book been read? Yes or No.");
            string answer = Console.ReadLine();
            int isBookRead;
            if (answer.ToLower() == "yes")
            {
                isBookRead = 1;
            }
            else
            {
                isBookRead = 0;
            }

            Console.WriteLine("Do you want to add description for the book?");
            answer = Console.ReadLine();
            string bookDescription;
            if (answer.ToLower() == "yes")
            {
                Console.WriteLine("Please enter description.");
                bookDescription = Console.ReadLine();
            }
            else
            {
                bookDescription = "";
            }

            Book newBook = new()
            {
                BookName = bookName,
                BookCategory = bookCategory,
                Writer = writer,
                BookDescription = bookDescription,
                ReadUnread = isBookRead
            };

            our_database.InsertData(newBook);
            continue;
        case 3:
            continue;
        case 4:
            Console.WriteLine("Please enter the id that you want to delete.");
            int prefferedId = Convert.ToInt32(Console.ReadLine());
            our_database.DeleteRow(prefferedId);
            continue;
        case 5:
            Console.WriteLine("Possible Actions:\n" +
            "List all data from database: 1\n" +
            "Insert data to database: 2\n" +
            "Update data from database: 3\n" +
            "Delete data from database: 4\n");
            continue;
    }
}
