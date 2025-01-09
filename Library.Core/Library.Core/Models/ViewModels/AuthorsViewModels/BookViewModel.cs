namespace Library.Core.Models.ViewModels.AuthorsViewModels
{
    public class AuthorViewModel
    {
        public Guid AGID { get; set; }
        public string AFirstName { get; set; }
        public string? AMiddleName { get; set; }
        public string ALastName { get; set; }
        public string? ANickName { get; set; }
        public string? ANationality { get; set; }
    }
}
