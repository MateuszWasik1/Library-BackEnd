﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.User.Queries;
using Library.Core.CQRS.Resources.User.Commands;
using Library.Core.Models.ViewModels.UserViewModels;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public UserController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public GetUsersAdminViewModel GetAllUsers(int skip, int take)
            => dispatcher.DispatchQuery<GetAllUsersQuery, GetUsersAdminViewModel>(new GetAllUsersQuery() { Skip = skip, Take = take });

        [HttpGet]
        [Route("GetUserByAdmin/{ugid}")]
        [Authorize(Roles = "Admin")]
        public UserAdminViewModel GetUserByAdmin(Guid ugid)
            => dispatcher.DispatchQuery<GetUserByAdminQuery, UserAdminViewModel>(new GetUserByAdminQuery() { UGID = ugid });

        [HttpGet]
        [Route("GetUser")]
        [Authorize]
        public UserViewModel GetUser()
            => dispatcher.DispatchQuery<GetUserQuery, UserViewModel>(new GetUserQuery());

        [HttpPost]
        [Route("SaveUser")]
        [Authorize]
        public void SaveUser(UserViewModel model)
            => dispatcher.DispatchCommand(new SaveUserCommand() { Model = model });

        [HttpPost]
        [Route("SaveUserByAdmin")]
        [Authorize(Roles = "Admin")]
        public void SaveUserByAdmin(UserAdminViewModel model)
            => dispatcher.DispatchCommand(new SaveUserByAdminCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteUser/{ugid}")]
        [Authorize(Roles = "Admin")]
        public void DeleteUser(Guid ugid)
            => dispatcher.DispatchCommand(new DeleteUserCommand() { UGID = ugid });
    }
}