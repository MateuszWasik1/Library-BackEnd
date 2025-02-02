namespace Library.Core.Exceptions.Reports { 
    public class ReportsExceptions : Exception
    {
        public ReportsExceptions(string message) : base(message)
        {
        }
    }

    public class ReportNotFoundException : ReportsExceptions
    {
        public ReportNotFoundException(string message) : base(message)
        {
        }
    }

    public class RNameMin3Characters : ReportsExceptions
    {
        public RNameMin3Characters(string message) : base(message)
        {
        }
    }

    public class RNameMax255Characters : ReportsExceptions
    {
        public RNameMax255Characters(string message) : base(message)
        {
        }
    }

    public class RBase64Min3Characters : ReportsExceptions
    {
        public RBase64Min3Characters(string message) : base(message)
        {
        }
    }
}