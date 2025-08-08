using LibraryManagementSystem.Events;
using LibraryManagementSystem.Exceptions;

namespace LibraryManagementSystem.Models
{
    public class Faculty : User
    {
        private const int BorrowLimit = 5;
 
        public override void BorrowBook(Book book)
        {
            if (BorrowedBooks.Count >= BorrowLimit)
            {
                LibraryEvents.OnBorrowLimitReached(this);
                throw new LimitReachedException($"Faculty {Name} has reached the borrow limit.");
            }

            book.Borrow(this);
            BorrowedBooks.Add(book);
        }

        public override void ReturnBook(Book book)
        {
            if (BorrowedBooks.Contains(book))
            {
                book.Return();
                BorrowedBooks.Remove(book);
            }
        }
    }
}
