﻿using Library.Core.Context;
using Library.Core.CQRS.Resources.User.Commands;
using Library.Core.CQRS.Resources.User.Handlers;
using Library.Core.Exceptions;
using Library.Core.Exceptions.Users;
using Library.Core.Models.Enums;
using Library.Core.Models.ViewModels.UserViewModels;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Library.UnitTests.CQRS.CommandHandlers.User
{
    [TestFixture]
    public class TestSaveUserByAdminCommandHandler
    {
        private Mock<IDataBaseContext>? context;

        private List<Core.Entities.User>? users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();

            users = new List<Core.Entities.User>
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    UGID = new Guid("98dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                    URID =  (int) RoleEnum.User,
                    UFirstName = "OldName",
                    ULastName = "OldLastName",
                    UUserName = "OldUserName",
                    UEmail = "OldEmail",
                    UPhone = "OldPhone",
                },
            };

            context.Setup(x => x.AllUsers).Returns(users.AsQueryable());

            context.Setup(x => x.CreateOrUpdate(It.IsAny<Core.Entities.User>())).Callback<Core.Entities.User>(user =>
            {
                var currentUser = users.FirstOrDefault(x => x.UID == user.UID);

                users[users.FindIndex(x => x.UID == currentUser.UID)] = user;
            });
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserNameIsEmpty_ShouldThrowUserNameRequiredException()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "",
                ULastName = "",
                UUserName = "",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserNameRequiredException>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserNameIsOver100_ShouldThrowUserNameMax100Exception()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "",
                ULastName = "",
                UUserName = "NewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserNameNewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserNameMax100Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserFirstnameIsOver50_ShouldThrowUserFirstNameMax50Exception()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "NewNameNewNameNewNameNewNameNewNameNewNameNewNameNewName",
                ULastName = "",
                UUserName = "NewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserFirstNameMax50Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserLastNameIsOver50_ShouldThrowUserLastNameMax50Exception()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastNameNewLastNameNewLastNameNewLastNameNewLastNameNewLastNameNewLastName",
                UUserName = "NewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserLastNameMax50Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserEmailIsEmpty_ShouldThrowUserEmailRequiredException()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "",
                UPhone = "",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserEmailRequiredException>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserEmailIsOver100_ShouldThrowUserEmailMax100Exception()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmailNewEmail",
                UPhone = "",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserEmailMax100Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserPhoneIsOver100_ShouldThrowUserPhoneMax100Exception()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmail",
                UPhone = "NewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhoneNewPhone",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserPhoneMax100Exception>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserNotFound_ShouldThrowUserNotFoundExceptions()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UID = 1,
                UGID = Guid.NewGuid(),
                URID = (int) RoleEnum.Admin,
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmail",
                UPhone = "NewPhone",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            //Assert
            Assert.Throws<UserNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestSaveUserByAdminCommandHandler_UserFound_ShouldUpdateUser()
        {
            //Arrange
            var model = new UserAdminViewModel()
            {
                UID = 1,
                UGID = new Guid("98dacc1d-7bee-4635-9c4c-9404a4af80dd"),
                URID = (int) RoleEnum.Admin,
                UFirstName = "NewName",
                ULastName = "NewLastName",
                UUserName = "NewUserName",
                UEmail = "NewEmail",
                UPhone = "NewPhone",
            };

            var query = new SaveUserByAdminCommand() { Model = model };
            var handler = new SaveUserByAdminCommandHandler(context.Object);

            //Act
            handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(4, users[0].URID);
            ClassicAssert.AreEqual("NewName", users[0].UFirstName);
            ClassicAssert.AreEqual("NewLastName", users[0].ULastName);
            ClassicAssert.AreEqual("NewUserName", users[0].UUserName);
            ClassicAssert.AreEqual("NewEmail", users[0].UEmail);
            ClassicAssert.AreEqual("NewPhone", users[0].UPhone);
        }
    }
}
