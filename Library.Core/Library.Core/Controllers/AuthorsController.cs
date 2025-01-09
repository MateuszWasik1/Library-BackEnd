using Microsoft.AspNetCore.Mvc;
using Library.Core.CQRS.Dispatcher;
using Library.Core.Models.ViewModels.AuthorsViewModels;
using Library.Core.CQRS.Resources.Authors.Commands;
using Library.Core.CQRS.Resources.Authors.Queries;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public AuthorsController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetAuthor")]
        public AuthorViewModel GetAuthor(Guid bgid)
            => dispatcher.DispatchQuery<GetAuthorQuery, AuthorViewModel>(new GetAuthorQuery() { BGID = bgid });

        [HttpGet]
        [Route("GetAuthors")]
        public AuthorsListViewModel GetAuthors(int skip, int take)
            => dispatcher.DispatchQuery<GetAuthorsQuery, AuthorsListViewModel>(new GetAuthorsQuery() { Skip = skip, Take = take });

        [HttpPost]
        [Route("AddAuthor")]
        public void AddAuthor(AuthorViewModel model)
            => dispatcher.DispatchCommand(new AddAuthorCommand() { Model = model });

        [HttpPut]
        [Route("UpdateAuthor")]
        public void UpdateAuthor(AuthorViewModel model)
            => dispatcher.DispatchCommand(new UpdateAuthorCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteAuthor/{bGID}")]
        public void Delete(Guid bGID)
             => dispatcher.DispatchCommand(new DeleteAuthorCommand() { BGID = bGID });
    }
}