using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    public class Reports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RID { get; set; }
        public Guid RGID { get; set; }
        public string? RName { get; set; }
        public DateTime RGenerationDate { get; set; }
        public string? RBase64 { get; set; }
    }
}