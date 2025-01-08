namespace Library.Core.Exceptions.Tasks
{
    public class UsersExceptions : Exception
    {
        public UsersExceptions(string message) : base(message)
        {
        }
    }

    public class UserNameRequiredException : UsersExceptions
    {
        public UserNameRequiredException(string message) : base(message)
        {
        }
    }

    public class UserNameMax100Exception : UsersExceptions
    {
        public UserNameMax100Exception(string message) : base(message)
        {
        }
    }

    public class UserFirstNameMax50Exception : UsersExceptions
    {
        public UserFirstNameMax50Exception(string message) : base(message)
        {
        }
    }

    public class UserLastNameMax50Exception : UsersExceptions
    {
        public UserLastNameMax50Exception(string message) : base(message)
        {
        }
    }

    public class UserPhoneMax100Exception : UsersExceptions
    {
        public UserPhoneMax100Exception(string message) : base(message)
        {
        }
    }

    public class UserEmailRequiredException : UsersExceptions
    {
        public UserEmailRequiredException(string message) : base(message)
        {
        }
    }

    public class UserEmailMax100Exception : UsersExceptions
    {
        public UserEmailMax100Exception(string message) : base(message)
        {
        }
    }
}