using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    public class Tests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TID { get; set; }
        public int TUID { get; set; }
        public string TName { get; set; }
    }
}