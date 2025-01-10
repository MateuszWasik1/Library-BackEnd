namespace Library.Core.Exceptions.Authors
{
    public class AuthorsExceptions : Exception
    {
        public AuthorsExceptions(string message) : base(message)
        {
        }
    }

    public class AuthorNotFoundException : AuthorsExceptions
    {
        public AuthorNotFoundException(string message) : base(message)
        {
        }
    }

    public class AFirstNameMin1CharacterException : AuthorsExceptions
    {
        public AFirstNameMin1CharacterException(string message) : base(message)
        {
        }
    }

    public class AFirstNameMax255CharacterException : AuthorsExceptions
    {
        public AFirstNameMax255CharacterException(string message) : base(message)
        {
        }
    }

    public class AMiddleNameMax255CharacterException : AuthorsExceptions
    {
        public AMiddleNameMax255CharacterException(string message) : base(message)
        {
        }
    }

    public class ALastNameMin1CharacterException : AuthorsExceptions
    {
        public ALastNameMin1CharacterException(string message) : base(message)
        {
        }
    }

    public class ALastNameMax255CharacterException : AuthorsExceptions
    {
        public ALastNameMax255CharacterException(string message) : base(message)
        {
        }
    }

    public class ANickNameMax255CharacterException : AuthorsExceptions
    {
        public ANickNameMax255CharacterException(string message) : base(message)
        {
        }
    }

    public class ANationalityMax255CharacterException : AuthorsExceptions
    {
        public ANationalityMax255CharacterException(string message) : base(message)
        {
        }
    }
}