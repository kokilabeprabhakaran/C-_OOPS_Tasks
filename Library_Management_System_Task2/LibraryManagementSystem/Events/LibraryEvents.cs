using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Events
{
    public static class LibraryEvents
    {
        public static event EventHandler<Book>? BookUnavailable;
        public static event EventHandler<User>? BorrowLimitReached;

        public static void OnBookUnavailable(Book book)
        {
            BookUnavailable?.Invoke(null, book);
        }

        public static void OnBorrowLimitReached(User user)
        {
            BorrowLimitReached?.Invoke(null, user);
        }
    }
}