using AutoMapper;
using Library.Core.Entities;
using Library.Core.Models.ViewModels.AuthorsViewModels;
using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.Models.ViewModels.PublishersViewModels;
using Library.Core.Models.ViewModels.ReportsViewModels;
using Library.Core.Models.ViewModels.UserViewModels;

namespace Library.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<User, UsersAdminViewModel>();
            CreateMap<User, UserAdminViewModel>();
            CreateMap<Books, BookViewModel>();
            CreateMap<Books, BooksViewModel>();
            CreateMap<Books, BooksListViewModel>();
            CreateMap<Authors, AuthorViewModel>();
            CreateMap<Authors, AuthorsViewModel>();
            CreateMap<Authors, AuthorsListViewModel>();
            CreateMap<Publishers, PublisherViewModel>();
            CreateMap<Publishers, PublishersViewModel>();
            CreateMap<Publishers, PublishersListViewModel>();
            CreateMap<Reports, ReportViewModel>();
            CreateMap<Reports, ReportsViewModel>();
            CreateMap<Reports, ReportsListViewModel>();
        }
    }
}