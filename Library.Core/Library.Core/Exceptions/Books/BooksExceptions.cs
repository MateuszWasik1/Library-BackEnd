namespace Library.Core.Exceptions.Books
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

    public class AuthorRequiredException : BooksExceptions
    {
        public AuthorRequiredException(string message) : base(message)
        {
        }
    }

    public class PublisherRequiredException : BooksExceptions
    {
        public PublisherRequiredException(string message) : base(message)
        {
        }
    }

    public class TitleNameMin3CharactersException : BooksExceptions
    {
        public TitleNameMin3CharactersException(string message) : base(message)
        {
        }
    }

    public class TitleNameMax255CharactersException : BooksExceptions
    {
        public TitleNameMax255CharactersException(string message) : base(message)
        {
        }
    }

    public class ISBNDifferentThan13CharactersException : BooksExceptions
    {
        public ISBNDifferentThan13CharactersException(string message) : base(message)
        {
        }
    }

    public class LanguageMax255CharactersException : BooksExceptions
    {
        public LanguageMax255CharactersException(string message) : base(message)
        {
        }
    }

    public class DescriptionMax2000CharactersException : BooksExceptions
    {
        public DescriptionMax2000CharactersException(string message) : base(message)
        {
        }
    }
}