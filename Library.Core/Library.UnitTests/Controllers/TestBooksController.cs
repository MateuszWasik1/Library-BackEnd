using Library.Core.Controllers;
using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.Books.Commands;
using Library.Core.CQRS.Resources.Books.Queries;
using Library.Core.Models.Enums;
using Library.Core.Models.ViewModels.BooksViewModels;
using Moq;
using NUnit.Framework;

namespace Library.UnitTests.Controllers
{
    [TestFixture]
    public class TestBooksController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void TestBooksController_GetBook_ShouldDispatch_GetBookQuery()
        {
            //Arrange
            var controller = new BooksController(dispatcher.Object);

            //Act
            controller.GetBook(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetBookQuery, BookViewModel>(It.IsAny<GetBookQuery>()), Times.Once);
        }

        [Test]
        public void TestBooksController_GetTooks_ShouldDispatch_GetBooksQuery()
        {
            //Arrange
            var controller = new BooksController(dispatcher.Object);

            //Act
            controller.GetBooks(0, 0, GenreEnum.All, new Guid(), new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetBooksQuery, BooksListViewModel>(It.IsAny<GetBooksQuery>()), Times.Once);
        }

        [Test]
        public void TestBooksController_AddBook_ShouldDispatch_AddBookCommand()
        {
            //Arrange
            var controller = new BooksController(dispatcher.Object);

            //Act
            controller.AddBook(new BookViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<AddBookCommand>()), Times.Once);
        }


        [Test]
        public void TestBooksController_UpdateBook_ShouldDispatch_UpdateBookCommand()
        {
            //Arrange
            var controller = new BooksController(dispatcher.Object);

            //Act
            controller.UpdateBook(new BookViewModel());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<UpdateBookCommand>()), Times.Once);
        }

        [Test]
        public void TestBooksController_Delete_ShouldDispatch_DeleteBookCommand()
        {
            //Arrange
            var controller = new BooksController(dispatcher.Object);

            //Act
            controller.Delete(new Guid());

            //Assert
            dispatcher.Verify(x => x.DispatchCommand(It.IsAny<DeleteBookCommand>()), Times.Once);
        }
    }
}