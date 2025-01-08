using Moq;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using Library.Core.Context;
using Library.Core.Services;
using Library.Core.Models.ViewModels.UserViewModels;
using Library.Core.Exceptions.Users;
using Library.Core.CQRS.Resources.User.Handlers;
using Library.Core.CQRS.Resources.User.Commands;
using Library.Core.Exceptions;
using Library.Core.Models.Enums;

namespace Library.UnitTests.CQRS.CommandHandlers.User
{
    [TestFixture]
    public class TestSaveUserCommandHandler
    {
        private Mock<IDataBaseContext>? context;
        private Mock<IUserContext>? user;

        private List<Core.Entities.User>? users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

            users = new List<Core.Entities.User>
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    UGID = new Guid("98dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    URID = (int) RoleEnum.User,
                    UFirstName = "OldName",
                    ULastName = "OldLastName",
                    UUserName = "OldUserName",
                    UEmail = "OldEmail",
                    UPhone = "OldPhone",
                },
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.User>())).Callback<Core.Entities.User>(user =>
            {
                var currentUser = users.FirstOrDefault(x => x.UID == user.UID);

                users[users.FindIndex(x => x.UID == currentUser.UID)] = user;
            });

            user.Setup(x => x.UID).Returns(1);
        }

        [Test]
        public void TestSaveUserCommandHandler_UserNameIsEmpty_ShouldThrowUserNameRequiredException()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "",
                ULastName = "",
                UUserName = "",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserNameRequiredException>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserNameIsOver100_ShouldThrowUserNameMax100Exception()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "",
                ULastName = "",
                UUserName = "NewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserNameMax100Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserFirstnameIsOver50_ShouldThrowUserFirstNameMax50Exception()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "NewNameNewNameNewNameNewNameNewNameNewNameNewNameNewName",
                ULastName = "",
                UUserName = "NewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserFirstNameMax50Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserLastNameIsOver50_ShouldThrowUserLastNameMax50Exception()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastNameNewLastNameNewLastNameNewLastNameNewLastNameNewLastNameNewLastName",
                UUserName = "NewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserLastNameMax50Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserEmailIsEmpty_ShouldThrowUserEmailRequiredException()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserEmailRequiredException>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserEmailIsOver100_ShouldThrowUserEmailMax100Exception()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmail",
                UPhone = "",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserEmailMax100Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserPhoneIsOver100_ShouldThrowUserPhoneMax100Exception()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmail",
                UPhone = "NewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhone",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserPhoneMax100Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserCommandHandler_UserNotFound_ShouldThrowUserNotFoundExceptions()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(2);

            var model = new UserViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmail",
                UPhone = "NewPhone",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            //Assert
            Assert.Throws<UserNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserFound_ShouldUpdateUser()
        {
            //Arrange
            var model = new UserViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmail",
                UPhone = "NewPhone",
            };

            var query = new SaveUserCommand() { Model = model };
            var handler = new SaveUserCommandHandler(context.Object, user.Object);

            //Act
            handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual("NewName", users[0].UFirstName);
            ClassicAssert.AreEqual("NewLastName", users[0].ULastName);
            ClassicAssert.AreEqual("NewUserName", users[0].UUserName);
            ClassicAssert.AreEqual("NewEmail", users[0].UEmail);
            ClassicAssert.AreEqual("NewPhone", users[0].UPhone);
        }
    }
}
