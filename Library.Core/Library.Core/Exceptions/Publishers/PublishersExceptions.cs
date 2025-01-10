namespace Library.Core.Exceptions.Publishers
{
    public class PublishersExceptions : Exception
    {
        public PublishersExceptions(string message) : base(message)
        {
        }
    }

    public class PublisherNotFoundException : PublishersExceptions
    {
        public PublisherNotFoundException(string message) : base(message)
        {
        }
    }
}