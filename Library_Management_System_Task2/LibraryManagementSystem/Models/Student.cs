using LibraryManagementSystem.Events;
using LibraryManagementSystem.Exceptions;

namespace LibraryManagementSystem.Models
{
    public class Student : User
    {
        private const int BorrowLimit = 3;
        public override void BorrowBook(Book book)
        {
            if (BorrowedBooks.Count >= BorrowLimit)
            {
                LibraryEvents.OnBorrowLimitReached(this);
                throw new LimitReachedException($"Student {Name} has reached the borrow limit.");
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
