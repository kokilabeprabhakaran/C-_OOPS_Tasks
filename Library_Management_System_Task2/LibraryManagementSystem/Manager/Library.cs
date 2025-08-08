using LibraryManagementSystem.Models;
using System.Linq;

namespace LibraryManagementSystem.Manager
{
    public class Library
    {
        private List<User> users = new();
        private List<Book> books = new();

        public void AddUser(User user) => users.Add(user);
        public void AddBook(Book book) => books.Add(book);

        public void BorrowBook(int userId, int bookId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var book = books.FirstOrDefault(b => b.BookId == bookId);
            user?.BorrowBook(book!);
        }

        public void ReturnBook(int userId, int bookId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var book = books.FirstOrDefault(b => b.BookId == bookId);
            user?.ReturnBook(book!);
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

        public List<Book> FilterByAuthor(string author) => books.Where(b => b.Author.Contains(author)).ToList();
        public List<Book> FilterByTitleKeyword(string keyword) => books.Where(b => b.Title.Contains(keyword)).ToList();
        public List<Book> FilterByAvailability(bool isAvailable) => books.Where(b => b.IsAvailable == isAvailable).ToList();
    }
}
