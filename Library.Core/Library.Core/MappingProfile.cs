using AutoMapper;
using Library.Core.Entities;
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
        }
    }
}