using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Library.Core.Context;
using Library.Core.CQRS.Resources.Books.Handlers;
using Library.Core.CQRS.Resources.Books.Queries;
using Library.Core.Exceptions.Books;
using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.Models.Enums;

namespace Library.UnitTests.CQRS.QueryHandlers.Books
{
    [TestFixture]
    public class TestGetBookQueryHandler
    {
        private Mock<IDataBaseContext>? context;
        private Mock<IMapper>? mapper;

        private List<Core.Entities.Books>? books;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            mapper = new Mock<IMapper>();

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

            mapper.Setup(m => m.Map<Core.Entities.Books, BookViewModel>(It.IsAny<Core.Entities.Books>()))
                .Returns((Core.Entities.Books book) =>
                    new BookViewModel()
                    {
                        BGID = book.BGID,
                        BAuthorGID = book.BAuthorGID,
                        BPublisherGID = book.BPublisherGID,
                        BLanguage = book.BLanguage,
                        BTitle = book.BTitle,
                        BISBN = book.BISBN,
                        BDescription = book.BDescription,
                        BGenre = book.BGenre,
                    }
                );
        }

        [Test]
        public void TestGetBookQueryHandler_BookNotFound_BookNotFoundException()
        {
            //Arrange
            var query = new GetBookQuery() { BGID = new Guid() };
            var handler = new GetBookQueryHandler(context.Object, mapper.Object);

            //Act
            //Assert
            Assert.Throws<BookNotFoundException>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetTaskQueryHandler_TaskWasFound_ShouldReturnTask()
        {
            //Arrange
            var query = new GetBookQuery() { BGID = books[0].BGID };
            var handler = new GetBookQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(books[0].BGID, result.BGID);
            ClassicAssert.AreEqual(books[0].BAuthorGID, result.BAuthorGID);
            ClassicAssert.AreEqual(books[0].BPublisherGID, result.BPublisherGID);
            ClassicAssert.AreEqual(books[0].BLanguage, result.BLanguage);
            ClassicAssert.AreEqual(books[0].BTitle, result.BTitle);
            ClassicAssert.AreEqual(books[0].BISBN, result.BISBN);
            ClassicAssert.AreEqual(books[0].BDescription, result.BDescription);
            ClassicAssert.AreEqual(books[0].BGenre, result.BGenre);
        }
    }
}