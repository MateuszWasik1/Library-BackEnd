using Library.Core.Context;
using Library.Core.CQRS.Resources.Accounts.Commands;
using Library.Core.CQRS.Resources.Accounts.Handlers;
using Library.Core.Exceptions.Accounts;
using Library.Core.Models.Enums;
using Library.Core.Models.ViewModels.AccountsViewModel;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Library.UnitTests.CQRS.QueryHandler.Accounts
{
    [TestFixture]
    public class TestRegisterUserCommandHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IPasswordHasher<Core.Entities.User>> hasher;

        private List<Core.Entities.User> users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            hasher = new Mock<IPasswordHasher<Core.Entities.User>>();

            users = new List<Core.Entities.User>()
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    UGID = new Guid("30dd879c-ee2f-11db-8314-0800200c9a66"),
                    URID = (int) RoleEnum.User,
                    UFirstName = "UFirstName1",
                    ULastName = "ULastName1",
                    UUserName = "Test1",
                    UPassword = "Password1",
                },
            };

            context.Setup(x => x.AllUsers).Returns(users.AsQueryable());

            hasher.Setup(x => x.HashPassword(It.IsAny<Core.Entities.User>(), It.IsAny<string>())).Returns("HashedPassword");

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.User>())).Callback<Core.Entities.User>(user => users.Add(user));
        }

        [Test]
        public void TestRegisterUserCommandHandler_UserNameIsEmptyString_ShouldThrowRegisterUserNameIsEmptyException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "",
                UEmail = "",
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterUserNameIsEmptyException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_UserNameIsOver100_ShouldThrowRegisterUserNameIsOver100Exception()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
                UEmail = "",
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterUserNameIsOver100Exception>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_UserNameIsNull_ShouldThrowRegisterUserNameIsEmptyException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = null,
                UEmail = "",
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterUserNameIsEmptyException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_EmailIsEmptyString_ShouldThrowRegisterEmailIsEmptyException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "",
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterEmailIsEmptyException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_EmailIsNull_ShouldThrowRegisterEmailIsEmptyException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = null,
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterEmailIsEmptyException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_EmaikIsEmptyString_ShouldThrowRegisterEmailIsOver100Exception()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "User",
                UEmail = "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890",
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterEmailIsOver100Exception>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordIsEmptyString_ShouldThrowRegisterPasswordIsEmptyException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = ""
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordIsEmptyException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordIsNull_ShouldThrowRegisterPasswordIsEmptyException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = null
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordIsEmptyException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordHasNoNumbers_ShouldThrowRegisterPasswordNoNumbersException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = "aaaa"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordNoNumbersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordHasNoUpperCase_ShouldThrowRegisterPasswordNoUpperCaseException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = "1111"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordNoUpperCaseException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordHasNoLowerCase_ShouldThrowRegisterPasswordNoLowerCaseException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = "AAA1111"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordNoLowerCaseException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordHasNoSpecialSign_ShouldThrowRegisterPasswordNoSpecialSignsException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = "AAaa111"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordNoSpecialSignsException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_PasswordHasNot8characters_ShouldThrowRegisterPasswordNo8charactersException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "NewUser",
                UEmail = "NewEmail",
                UPassword = "AAaa11@"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterPasswordNo8charactersException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_UserNameExistingInSystem_ShouldThrowRegisterUserNameIsFoundException()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = users[0].UUserName,
                UEmail = "NewEmail",
                UPassword = "AAaaaa11@"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            //Assert
            Assert.Throws<RegisterUserNameIsFoundException>(() => handler.Handle(command));
        }

        [Test]
        public void TestRegisterUserCommandHandler_UserWasAdded_ShouldAddNewUser()
        {
            //Arrange 
            var model = new RegisterViewModel()
            {
                UUserName = "New User",
                UEmail = "NewEmail",
                UPassword = "AAaaaa11@Password"
            };

            var command = new RegisterUserCommand() { Model = model };
            var handler = new RegisterUserCommandHandler(context.Object, hasher.Object);

            //Act
            handler.Handle(command);

            //Assert

            ClassicAssert.AreEqual(2, users.Count);
            ClassicAssert.AreEqual("New User", users[1].UUserName);
            ClassicAssert.AreEqual("NewEmail", users[1].UEmail);
            ClassicAssert.AreEqual("HashedPassword", users[1].UPassword);
        }
    }
}