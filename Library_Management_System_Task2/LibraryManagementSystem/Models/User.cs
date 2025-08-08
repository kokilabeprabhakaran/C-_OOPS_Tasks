namespace LibraryManagementSystem.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        protected List<Book> BorrowedBooks = new();
        public IReadOnlyList<Book> GetBorrowedBooks()
        {
            return BorrowedBooks.AsReadOnly();
        }

        public abstract void BorrowBook(Book book);
        public abstract void ReturnBook(Book book);
    }
}