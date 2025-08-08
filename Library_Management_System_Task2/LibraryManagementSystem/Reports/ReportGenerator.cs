using LibraryManagementSystem.Models;
using LibraryManagementSystem.Manager;

namespace LibraryManagementSystem.Reports
{
    public class ReportGenerator
    {
        public void GenerateReport(Library library)
        {
            var available = library.ListAvailableBooks().Count;
            var borrowed = library.ListBorrowedBooks().Count;
            var total = available + borrowed;

            Console.WriteLine("\n=== Library Report ===");
            Console.WriteLine($"Total Books: {total}");
            Console.WriteLine($"Available Books: {available}");
            Console.WriteLine($"Borrowed Books: {borrowed}");
        }

        public void Top3UsersByBorrowCount(List<User> users)
        {
            Console.WriteLine("\nTop 3 Users by Borrow Count:");
            var topUsers = users.OrderByDescending(u => u.GetBorrowedBooks().Count).Take(3);

            foreach (var user in topUsers)
            {
                Console.WriteLine($"{user.Name}: {user.GetBorrowedBooks().Count} books");
            }
        }
    }
}
