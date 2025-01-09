using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    public class Authors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AID { get; set; }
        public Guid AGID { get; set; }
        public string AFirstName { get; set; }
        public string? AMiddleName { get; set; }
        public string ALastName { get; set; }
        public string? ANickName { get; set; }
        public string? ANationality { get; set; }
    }
}