using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Events
{
    public class BookEventArgs : EventArgs
    {
        public Book Book { get; set; }
        public User User { get; set; }

        public BookEventArgs(Book book, User user)
        {
            Book = book;
            User = user;
        }
    }
}