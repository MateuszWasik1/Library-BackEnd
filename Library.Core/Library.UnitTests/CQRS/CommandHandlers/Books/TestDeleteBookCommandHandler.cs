using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Library.Core.Context;
using Library.Core.CQRS.Resources.Books.Handlers;
using Library.Core.CQRS.Resources.Books.Commands;
using Library.Core.Exceptions.Books;
using Library.Core.Models.Enums;

namespace Library.UnitTests.CQRS.CommandHandlers.Books
{
    [TestFixture]
    public class TestDeleteBookCommandHandler
    {
        private Mock<IDataBaseContext>? context;

        private List<Core.Entities.Books>? books;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            books = new List<Core.Entities.Books>()
            {
                new Core.Entities.Books()
                {
                    BID = 1,
                    BGID = new Guid("f3dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BAuthorGID = new Guid("f4dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BPublisherGID = new Guid("f5dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BUID = 1,
                    BLanguage = "Polski",
                    BTitle = "Tytuł 1",
                    BISBN = "1234567890123",
                    BDescription = "Description 1",
                    BGenre = GenreEnum.Fantasy,
                },
                new Core.Entities.Books()
                {
                    BID = 2,
                    BGID = new Guid("f6dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BAuthorGID = new Guid("f7dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BPublisherGID = new Guid("f8dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BUID = 1,
                    BLanguage = "Angielski",
                    BTitle = "Tytuł 2",
                    BISBN = "1234567890124",
                    BDescription = "Description 2",
                    BGenre = GenreEnum.Fantasy,
                },
                new Core.Entities.Books()
                {
                    BID = 3,
                    BGID = new Guid("f9dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BAuthorGID = new Guid("f0dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BPublisherGID = new Guid("f1dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    BUID = 1,
                    BLanguage = "Chiński",
                    BTitle = "Tytuł 3",
                    BISBN = "1234567890125",
                    BDescription = "Description 3",
                    BGenre = GenreEnum.Fantasy,
                }
            };

            context.Setup(x => x.AllBooks).Returns(books.AsQueryable());

            context.Setup(x => x.DeleteBook(It.IsAny<Core.Entities.Books>())).Callback<Core.Entities.Books>(book => books.Remove(book));
        }

        [Test]
        public void TestDeleteBookCommandHandler_DeleteBook_BookNotFound_BookNotFoundException()
        {
            //Arrange
            var command = new DeleteBookCommand() { BGID = Guid.Empty };
            var handler = new DeleteBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<BookNotFoundException>(() => handler.Handle(command));
        }

        [Test]
        public void TestDeleteBookCommandHandler_DeleteBook_BookIsFound_ShouldDeleteBook()
        {
            //Arrange
            var command = new DeleteBookCommand() { BGID = books[2].BGID };
            var handler = new DeleteBookCommandHandler(context.Object);

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(2, books.Count);
            ClassicAssert.IsFalse(books.Any(x => x.BGID == new Guid("f9dacc1d-7bee-4635-9c4c-9404a4af80dd")));
        }
    }
}