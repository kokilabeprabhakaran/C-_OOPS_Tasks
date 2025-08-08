namespace LibraryManagementSystem.Exceptions
{
    public class LimitReachedException : Exception
    {
        public LimitReachedException(string message) : base(message)
        { 
            
        }
    }
}