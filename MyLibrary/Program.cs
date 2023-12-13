using System;
using System.Drawing;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

//Connection to the database
DatabaseOperations our_database = new();

Console.WriteLine("Welcome to the App\n");

Console.WriteLine("Possible Actions:\n" +
                  "List all data from database: 1\n" +
                  "Insert data to database: 2\n" +
                  "Update data from database: 3\n" +
                  "Delete data from database: 4\n");

var result = -1;
while (result != 0)
{
    Console.WriteLine("Please enter preferred action. 0 for exit. 5 for help.");
    var text = Console.ReadLine();
    var isNumber = Int32.TryParse(text, out result);
    if (!isNumber)
    {
        Console.WriteLine("That is not a valid number. Program will be terminated");
        break;
    }
    else if (isNumber && (result < 0 || result >= 6))
        Console.WriteLine("That is not a valid number. Try again");
    else
    {
        switch (result)
        {
            case 0:

                our_database.CloseConnection();
                break;

            case 1:

                our_database.ReadData();
                continue;

            case 2:

                #region Add Book
                string bookName = "";
                string bookCategory = "";
                string writer = "";
                int isBookRead = 0;
                string bookDescription = "";

                NewBook(ref bookName, ref bookCategory, ref writer, ref isBookRead, ref bookDescription);

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
            #endregion

            case 3:

                #region Update Book
                Console.WriteLine("Please enter ID that you want to update.");
                int selectedId = Convert.ToInt32(Console.ReadLine());

                if (Int32.Parse(our_database.CheckIdAvailable(selectedId)) == 1)
                {
                    Console.WriteLine(
                        $"You have selected {selectedId}. Which column you want to update? Please write corresponding number.");
                    Console.WriteLine(
                        "Book Name: 1\nBook Category: 2\nBook Writer: 3\nBook Description: 4\nIs Book Active: 5\nIs Book Read: 6\nTo Cancel: 7\n");

                    var columnInput = Console.ReadLine();
                    var whichColumnChange = -1;
                    var isNumberColumn = Int32.TryParse(columnInput, out whichColumnChange);

                    if (!isNumberColumn)
                    {
                        Console.WriteLine("That is not a valid number. Program will be terminated.");
                        break;
                    }
                    else if (isNumberColumn && (whichColumnChange <= 0 || whichColumnChange >= 7))
                    {
                        Console.WriteLine("That is not a valid number. Try again.");
                    }
                    else
                    {
                        Console.WriteLine("Please write new value:");
                        string newValue = Console.ReadLine();

                        switch (whichColumnChange)
                        {
                            case 1:
                                our_database.UpdateData(selectedId, "BookName", newValue);
                                break;

                            case 2:
                                our_database.UpdateData(selectedId, "BookCategory", newValue);
                                break;

                            case 3:
                                our_database.UpdateData(selectedId, "Writer", newValue);
                                break;

                            case 4:
                                our_database.UpdateData(selectedId, "BookDescription", newValue);
                                break;

                            case 5:
                                our_database.UpdateData(selectedId, "ActiveStatus", newValue);
                                break;

                            case 6:
                                our_database.UpdateData(selectedId, "ReadUnread", newValue);
                                break;
                        }
                    }

                }
                else
                {
                    Console.WriteLine($"{selectedId} Id is not available.\n");
                    break;
                }

                continue;
            #endregion

            case 4:

                #region Delete Operation
                Console.WriteLine("Please enter the id that you want to delete.");
                int prefferedId = Convert.ToInt32(Console.ReadLine());
                our_database.DeleteRow(prefferedId);
                continue;
            #endregion

            case 5:

                #region Help Menu
                Console.WriteLine("Possible Actions:\n" +
                                  "List all data from database: 1\n" +
                                  "Insert data to database: 2\n" +
                                  "Update data from database: 3\n" +
                                  "Delete data from database: 4\n");
                continue;
                #endregion
        }
    }
}

static void NewBook(ref string bookName, ref string bookCategory, ref string writer, ref int isBookRead, ref string bookDescription)
{
    Console.WriteLine("Please enter name of the new book.");
    bookName = Console.ReadLine();

    Console.WriteLine("Please enter category of the new book.");
    bookCategory = Console.ReadLine();

    Console.WriteLine("Please enter writers name.");
    writer = Console.ReadLine();

    Console.WriteLine("Has book been read? Yes or No.");
    string answer = Console.ReadLine();

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

    if (answer.ToLower() == "yes")
    {
        Console.WriteLine("Please enter description.");
        bookDescription = Console.ReadLine();
    }
    else
    {
        bookDescription = "";
    }
}