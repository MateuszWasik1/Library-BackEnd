namespace Library.Core.Models.ViewModels.ReportsViewModels
{
    public class ReportViewModel
    {
        public Guid RGID { get; set; }
        public string? RName { get; set; }
        public DateTime RGenerationDate { get; set; }
        public string? RBase64 { get; set; }
    }
}
