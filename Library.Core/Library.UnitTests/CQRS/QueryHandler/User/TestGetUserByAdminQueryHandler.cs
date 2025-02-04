﻿using AutoMapper;
using Library.Core.Context;
using Library.Core.CQRS.Resources.User.Handlers;
using Library.Core.CQRS.Resources.User.Queries;
using Library.Core.Exceptions;
using Library.Core.Models.Enums;
using Library.Core.Models.ViewModels.UserViewModels;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Library.UnitTests.CQRS.QueryHandler.User
{
    [TestFixture]
    public class TestGetUserByAdminQueryHandler
    {
        private Mock<IDataBaseContext>? context;
        private Mock<IMapper>? mapper;

        private List<Core.Entities.User>? users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            mapper = new Mock<IMapper>();

            users = new List<Core.Entities.User>()
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    UGID = new Guid("b189857a-bf45-4c25-9644-f2408351d328"),
                    URID = (int) RoleEnum.Support,
                    UFirstName = "UFirstName1",
                    ULastName = "ULastName1",
                    UUserName = "UUserName1",
                    UEmail = "UEmail1",
                    UPhone = "UPhone1",
                },
                new Core.Entities.User()
                {
                    UID = 2,
                    UGID = new Guid("c189857a-bf45-4c25-9644-f2408351d328"),
                    URID = (int) RoleEnum.Premium,
                    UFirstName = "UFirstName2",
                    ULastName = "ULastName2",
                    UUserName = "UUserName2",
                    UEmail = "UEmail2",
                    UPhone = "UPhone2",
                },
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());
            context.Setup(x => x.AllUsers).Returns(users.AsQueryable());

            mapper.Setup(m => m.Map<Core.Entities.User, UserAdminViewModel>(It.IsAny<Core.Entities.User>())).Returns(new UserAdminViewModel()
            {
                UID = users[0].UID,
                UGID = users[0].UGID,
                URID = users[0].URID,
                UFirstName = users[0].UFirstName,
                ULastName = users[0].ULastName,
                UUserName = users[0].UUserName,
                UEmail = users[0].UEmail,
                UPhone = users[0].UPhone,
            });
        }

        [Test]
        public void TestGetUserByAdminQueryHandler_GetUserByAdmin_UserNotFound_ShouldThrowUserNotFoundExceptions()
        {
            //Arrange
            var query = new GetUserByAdminQuery() { UGID = new Guid("d189857a-bf45-4c25-9644-f2408351d328") };
            var handler = new GetUserByAdminQueryHandler(context.Object, mapper.Object);

            //Act
            //Assert
            Assert.Throws<UserNotFoundExceptions>(() => handler.Handle(query));
        }

        [Test]
        public void TestGetUserByAdminQueryHandler_GetUserByAdmin_ShouldReturnUser()
        {
            //Arrange
            var query = new GetUserByAdminQuery() { UGID = new Guid("b189857a-bf45-4c25-9644-f2408351d328") };
            var handler = new GetUserByAdminQueryHandler(context.Object, mapper.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.AreEqual(1, result.UID);
            ClassicAssert.AreEqual(new Guid("b189857a-bf45-4c25-9644-f2408351d328"), result.UGID);
            ClassicAssert.AreEqual(3, result.URID);
            ClassicAssert.AreEqual("UFirstName1", result.UFirstName);
            ClassicAssert.AreEqual("ULastName1", result.ULastName);
            ClassicAssert.AreEqual("UUserName1", result.UUserName);
            ClassicAssert.AreEqual("UEmail1", result.UEmail);
            ClassicAssert.AreEqual("UPhone1", result.UPhone);
        }
    }
}
