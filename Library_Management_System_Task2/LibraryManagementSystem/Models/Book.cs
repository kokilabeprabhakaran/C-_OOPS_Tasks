using LibraryManagementSystem.Events;
using LibraryManagementSystem.Exceptions;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        // Auto implemented properties
        public User? BorrowedBy { get; private set; }
        public bool IsAvailable { get; private set; } = true;


        // private bool isAvailable = true;
        // public bool IsAvailable
        // {
        //     get { return isAvailable; }
        // }

        public void Borrow(User user)
        {
            if (!IsAvailable) throw new InvalidOperationException("Book is not available.");
            IsAvailable = false;
            BorrowedBy = user;
            LibraryEvents.OnBookUnavailable(this);
        }

        public void Return()
        {
            IsAvailable = true;
            BorrowedBy = null;
        }
    }
}