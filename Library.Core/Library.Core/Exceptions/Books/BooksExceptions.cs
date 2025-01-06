namespace Library.Core.Exceptions.Tasks
{
    public class BooksExceptions : Exception
    {
        public BooksExceptions(string message) : base(message)
        {
        }
    }

    public class BookNotFoundException : BooksExceptions
    {
        public BookNotFoundException(string message) : base(message)
        {
        }
    }
}