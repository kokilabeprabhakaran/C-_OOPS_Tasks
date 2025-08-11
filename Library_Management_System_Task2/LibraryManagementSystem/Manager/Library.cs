using LibraryManagementSystem.Models;
using System.Linq;

namespace LibraryManagementSystem.Manager
{
    public class Library
    {
        private List<User> users = new();
        private List<Book> books = new();

        public void AddUser(User user)
        {
            if (users.Any(u => u.Id == user.Id))
            {
                Console.WriteLine($"User with ID {user.Id} already exists.");
                return;
            }
            users.Add(user);
            Console.WriteLine("User added successfully.");
        }

        public void AddBook(Book book)
        {
            if (books.Any(b => b.BookId == book.BookId))
            {
                Console.WriteLine($"Book with ID {book.BookId} already exists.");
                return;
            }

            books.Add(book);
            Console.WriteLine("Book added successfully.");
        }


        public void BorrowBook(int userId, int bookId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var book = books.FirstOrDefault(b => b.BookId == bookId);
            if (user != null && book != null)
            {
                user.BorrowBook(book);
            }
        }

        public void ReturnBook(int userId, int bookId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var book = books.FirstOrDefault(b => b.BookId == bookId);
            if (user != null && book != null)
            {
                user.ReturnBook(book);
            }
        }

        public List<Book> ListAvailableBooks() => books.Where(b => b.IsAvailable).ToList();
        public List<Book> ListBorrowedBooks() => books.Where(b => !b.IsAvailable).ToList();
        public List<Book> GetBorrowedBooksByUser(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            return user?.GetBorrowedBooks().ToList() ?? new List<Book>();
        }

        public IEnumerable<IGrouping<string, Book>> GroupBooksByAuthor()
        {
            return books.GroupBy(b => b.Author);
        }
        
        public List<User> GetAllUsers()
        {
            return users;
        }

        
    }
}
