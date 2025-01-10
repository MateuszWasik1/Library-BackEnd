using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    public class Publishers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID { get; set; }
        public Guid PGID { get; set; }
        public string PName { get; set; }
        public string? PCountry { get; set; }
        public string? PCity { get; set; }
        public string? PEmail { get; set; }
        public string? PPhone { get; set; }
    }
}