namespace Library.Core.Models.ViewModels.PublishersViewModels
{
    public class PublishersViewModel
    {
        public int PID { get; set; }
        public Guid PGID { get; set; }
        public string PName { get; set; }
        public string? PCountry { get; set; }
        public string? PCity { get; set; }
        public string? PEmail { get; set; }
        public string? PPhone { get; set; }
    }
}
