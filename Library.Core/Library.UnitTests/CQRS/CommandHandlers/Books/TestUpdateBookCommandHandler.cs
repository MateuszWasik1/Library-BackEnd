using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Library.Core.Context;
using Library.Core.Services;
using Library.Core.Models.ViewModels.BooksViewModels;
using Library.Core.CQRS.Resources.Books.Handlers;
using Library.Core.CQRS.Resources.Books.Commands;
using Library.Core.Exceptions.Books;
using Library.Core.Models.Enums;

namespace Library.UnitTests.CQRS.CommandHandlers.Books
{
    [TestFixture]
    public class TestUpdateBookCommandHandler
    {
        private Mock<IDataBaseContext>? context;
        private Mock<IUserContext>? user;

        private List<Core.Entities.Books>? books;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

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

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.Books>())).Callback<Core.Entities.Books>(book => books[books.FindIndex(x => x.BID == book.BID)] = book);
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BAuthorGID_IsEmpty_ShouldThrow_AuthorRequiredException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.Empty,
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<AuthorRequiredException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BPublisherGID_IsEmpty_ShouldThrow_PublisherRequiredException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.Empty,
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<PublisherRequiredException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BTitle_IsShorterThan3Characters_ShouldThrow_TitleNameMin3CharactersException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "1"
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<TitleNameMin3CharactersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BTitle_IsShorterLongerThan255Characters_ShouldThrow_TitleNameMax255CharactersException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "Rn5V3F1oEF2LVZiIoz6KNfe5rBkdAYEUnYFQEQa3geFnf4HcJJBHHvezIEEt4C529Db9Xk9mVtYlydyl2KD6ikh3m4ITcxSI3KstbnyL5PUH1QurhO0cr2KcFCBWr8YsGgJpf62tSaTcfSvyLecQVbCWb8yewDO3uoI1RNxgxYRyCzOtRmedC7id6EkXG1HcbiAh0z5sTudk93SMJCDwEuRtiXhiznRgNyTL0Xp1RVXNEPjk7MzYuLxc3vk3RAWl\r\n"
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<TitleNameMax255CharactersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BISBN_IsNot13Characters_ShouldThrow_ISBNDifferentThan13CharactersException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "123456789",
                BISBN = "12345"
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<ISBNDifferentThan13CharactersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BLanguage_IsLongerThan255Characters_ShouldThrow_LanguageMax255CharactersException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "123456789",
                BISBN = "1234567890123",
                BLanguage = "Rn5V3F1oEF2LVZiIoz6KNfe5rBkdAYEUnYFQEQa3geFnf4HcJJBHHvezIEEt4C529Db9Xk9mVtYlydyl2KD6ikh3m4ITcxSI3KstbnyL5PUH1QurhO0cr2KcFCBWr8YsGgJpf62tSaTcfSvyLecQVbCWb8yewDO3uoI1RNxgxYRyCzOtRmedC7id6EkXG1HcbiAh0z5sTudk93SMJCDwEuRtiXhiznRgNyTL0Xp1RVXNEPjk7MzYuLxc3vk3RAWl\r\n"
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<LanguageMax255CharactersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BDescription_IsLongerThan2000Characters_ShouldThrow_DescriptionMax2000CharactersException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "123456789",
                BISBN = "1234567890123",
                BLanguage = "1234",
                BDescription = "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\r\n"
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<DescriptionMax2000CharactersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_BookNotFound_ShouldThrow_BookNotFoundException()
        {
            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "123456789",
                BISBN = "1234567890123",
                BLanguage = "1234",
                BDescription = "12345",
                BGID = Guid.Empty,
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<BookNotFoundException>(() => handler.Handle(command));
        }


        [Test]
        public void TestUpdateBookCommandHandler_UpdateBook_ShouldUpdateBook()
        {

            //Arrange
            var model = new BookViewModel()
            {
                BAuthorGID = Guid.NewGuid(),
                BPublisherGID = Guid.NewGuid(),
                BTitle = "123456789",
                BISBN = "1234567890123",
                BLanguage = "12345",
                BDescription = "12345",
                BGID = books[2].BGID
            };

            var command = new UpdateBookCommand() { Model = model };
            var handler = new UpdateBookCommandHandler(context.Object);

            //Act
            handler.Handle(command);

            //Assert
            ClassicAssert.AreEqual(3, books.Count);
            ClassicAssert.AreEqual("123456789", books[2].BTitle);
            ClassicAssert.AreEqual("1234567890123", books[2].BISBN);
            ClassicAssert.AreEqual("12345", books[2].BLanguage);
            ClassicAssert.AreEqual("12345", books[2].BDescription);
        }
    }
}