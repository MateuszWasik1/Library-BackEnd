﻿using Library.Core.Models.Enums;

namespace Library.Core.Models.ViewModels.BooksViewModels
{
    public class BookViewModel
    {
        public Guid BGID { get; set; }
        public Guid BAuthorGID { get; set; }
        public Guid BPublisherGID { get; set; }
        public string BTitle { get; set; }
        public string BISBN { get; set; }
        public GenreEnum BGenre { get; set; }
        public string BLanguage { get; set; }
        public string? BDescription { get; set; }
    }
}
