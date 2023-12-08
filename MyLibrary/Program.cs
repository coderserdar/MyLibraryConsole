﻿using System;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

//Connection to the database
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
            Console.WriteLine("Please enter ID that you want to update.");
            int selectedId = Convert.ToInt32(Console.ReadLine());

            if (Int32.Parse(our_database.CheckIdAvailable(selectedId)) == 1)
            {
                Console.WriteLine($"You have selected {selectedId}. Which column you want to update? Please write corresponding number.");
                Console.WriteLine("Book Name: 1\nBook Category: 2\nBook Writer: 3\nBook Description: 4\nIs Book Active: 5\nIs Book Read: 6\nTo Cancel: 0\n");

                int whichColumnChange = Convert.ToInt32(Console.ReadLine());
                string newValue;

                switch (whichColumnChange)
                {
                    case 1:
                        Console.WriteLine("Please write new value:");
                        newValue = Console.ReadLine();
                        our_database.UpdateData(selectedId, "BookName", newValue);
                        break;

                    case 2:
                        Console.WriteLine("Please write new value:");
                        newValue = Console.ReadLine();
                        our_database.UpdateData(selectedId, "BookCategory", newValue);
                        break;

                    case 3:
                        Console.WriteLine("Please write new value:");
                        newValue = Console.ReadLine();
                        our_database.UpdateData(selectedId, "Writer", newValue);
                        break;

                    case 4:
                        Console.WriteLine("Please write new value:");
                        newValue = Console.ReadLine();
                        our_database.UpdateData(selectedId, "BookDescription", newValue);
                        break;

                    case 5:
                        Console.WriteLine("Please write new value:");
                        newValue = Console.ReadLine();
                        our_database.UpdateData(selectedId, "ActiveStatus", newValue);
                        break;

                    case 6:
                        Console.WriteLine("Please write new value:");
                        newValue = Console.ReadLine();
                        our_database.UpdateData(selectedId, "ReadUnread", newValue);
                        break;

                    case 0:
                        break;

                    default:
                        Console.WriteLine("Please enter any given number.");
                        continue;
                }
            }
            else
            {
                Console.WriteLine($"{selectedId} Id is not available.\n");
                break;
            }
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
