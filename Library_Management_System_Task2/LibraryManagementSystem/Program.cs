using LibraryManagementSystem.Models;
using LibraryManagementSystem.Manager;
using LibraryManagementSystem.Reports;
using LibraryManagementSystem.Events;

namespace LibraryManagementSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            Library library = new();
            ReportGenerator reportGenerator = new();

            LibraryEvents.BookUnavailable += (s, book) =>
            {
                Console.WriteLine($"[Event] Book Unavailable: {book.Title}");
            };
            LibraryEvents.BorrowLimitReached += (s, user) =>
            {
                Console.WriteLine($"[Event] Borrow Limit Reached for: {user.Name}");
            };

            bool running = true;
            while (running)
            {
                Console.WriteLine("Select an option");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Add Book");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. List Available Books");
                Console.WriteLine("6. List Borrowed Books");
                Console.WriteLine("7. Get Borrowed Books by User");
                Console.WriteLine("8. Group Books by Author");
                Console.WriteLine("9. Generate Report");
                Console.WriteLine("10. Top 3 Users by Borrow Count");
                Console.WriteLine("11. Exit");
                Console.Write("Enter your choice: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("-------Add User-------");
                        Console.Write("Enter User ID: ");
                        int userId = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine()!;
                        Console.Write("Enter Type (Student/Faculty): ");
                        string type = Console.ReadLine()!;
                        User user = type.ToLower() == "faculty" ? new Faculty { Id = userId, Name = name } : new Student { Id = userId, Name = name };
                        library.AddUser(user);
                    
                        break;

                    case "2":
                        Console.WriteLine("-------Add Book-------");
                        Console.Write("Enter Book ID: ");
                        int bookId = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine()!;
                        Console.Write("Enter Author: ");
                        string author = Console.ReadLine()!;
                        library.AddBook(new Book { BookId = bookId, Title = title, Author = author });
                        break;

                    case "3":
                        Console.WriteLine("-------Borrow Book-------");
                        Console.Write("Enter User ID: ");
                        userId = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter Book ID: ");
                        bookId = int.Parse(Console.ReadLine()!);
                        try { library.BorrowBook(userId, bookId); Console.WriteLine("Book borrowed."); } catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case "4":
                        Console.WriteLine("-------Return Book-------");
                        Console.Write("Enter User ID: ");
                        userId = int.Parse(Console.ReadLine()!);
                        Console.Write("Enter Book ID: ");
                        bookId = int.Parse(Console.ReadLine()!);
                        try { library.ReturnBook(userId, bookId); Console.WriteLine("Book returned."); } catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;

                    case "5":
                        Console.WriteLine("-------Available Books-------");
                        Console.WriteLine("\nAvailable Books:");
                        foreach (var book in library.ListAvailableBooks())
                            Console.WriteLine($"{book.BookId} - {book.Title} by {book.Author}");
                        Console.WriteLine("-------------------");
                        break;

                    case "6":
                        Console.WriteLine("-------Borrowed Books-------");
                        Console.WriteLine("\nBorrowed Books:");
                        foreach (var book in library.ListBorrowedBooks())
                            Console.WriteLine($"{book.BookId} - {book.Title} by {book.Author} (Borrowed by {book.BorrowedBy?.Name})");
                        Console.WriteLine("-------------------");
                        break;

                    case "7":
                        Console.WriteLine("-------Borrowed books by user-------");
                        Console.Write("Enter User ID: ");
                        userId = int.Parse(Console.ReadLine()!);
                        var borrowedBooks = library.GetBorrowedBooksByUser(userId);
                        Console.WriteLine($"\nBooks borrowed by user {userId}:");
                        foreach (var book in borrowedBooks)
                            Console.WriteLine($"{book.BookId} - {book.Title}");
                        Console.WriteLine("-------------------");
                        break;

                    case "8":
                        Console.WriteLine("-------Groped by author-------");
                        Console.WriteLine("\nBooks Grouped by Author:");
                        var groups = library.GroupBooksByAuthor();
                        foreach (var group in groups)
                        {
                            Console.WriteLine($"Author: {group.Key}");
                            foreach (var book in group)
                                Console.WriteLine($"  - {book.Title}");
                        }
                        Console.WriteLine("-------------------");
                        break;

                    case "9":
                        Console.WriteLine("-------Report-------");
                        reportGenerator.GenerateReport(library);
                        Console.WriteLine("-------------------");
                        break;
                    
                    case "10":
                        Console.WriteLine("-------Top 3 Users by Borrow Count-------");
                        var allUsers = library.GetAllUsers();
                        reportGenerator.Top3UsersByBorrowCount(allUsers);
                        Console.WriteLine("-------------------");
                        break;

                    case "11":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

        }
    }
}