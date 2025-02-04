﻿using Library.Core.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BID { get; set; }
        public Guid BGID { get; set; }
        public Guid BAuthorGID { get; set; }
        public Guid BPublisherGID { get; set; }
        public int BUID { get; set; }
        public string BTitle { get; set; }
        public string BISBN { get; set; }
        public GenreEnum BGenre { get; set; }
        public string BLanguage { get; set; }
        public string? BDescription { get; set; }
    }
}