using Microsoft.AspNetCore.Mvc;
using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.Accounts.Queries;
using Library.Core.Entities;
using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.CQRS.Resources.Books.Commands;

namespace Library.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        public BooksController(IDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpGet]
        [Route("GetBook")]
        public BookViewModel GetBook(Guid bgid)
            => dispatcher.DispatchQuery<GetBugQuery, BookViewModel>(new GetBugQuery() { BGID = bgid });

        [HttpGet]
        [Route("GetBooks")]
        public BooksViewModel GetBooks(BugTypeEnum bugType, int skip, int take)
            => dispatcher.DispatchQuery<GetBugsQuery, BooksViewModel>(new GetBugsQuery() { BugType = bugType, Skip = skip, Take = take });

        [HttpPost]
        [Route("AddBook")]
        public void AddBook(BookViewModel model)
            => dispatcher.DispatchCommand(new AddBookCommand() { Model = model });

        [HttpPut]
        [Route("UpdateBook")]
        public void UpdateBook(BookViewModel model)
            => dispatcher.DispatchCommand(new UpdateBookCommand() { Model = model });

        [HttpDelete]
        [Route("DeleteBook/{bGID}")]
        public void Delete(Guid bGID)
             => dispatcher.DispatchCommand(new DeleteBookCommand() { BGID = bGID });
    }
}