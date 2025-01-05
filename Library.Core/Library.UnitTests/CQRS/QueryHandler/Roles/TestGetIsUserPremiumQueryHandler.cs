using Library.Core.Context;
using Library.Core.CQRS.Resources.Roles.Handlers;
using Library.Core.CQRS.Resources.Roles.Queries;
using Library.Core.Models.Enums;
using Library.Core.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Library.UnitTests.CQRS.QueryHandler.Roles
{
    [TestFixture]
    public class TestGetIsUserPremiumQueryHandler
    {
        private Mock<IDataBaseContext> context;
        private Mock<IUserContext> user;

        private List<Core.Entities.User> users;

        [SetUp]
        public void SetUp()
        {
            context = new Mock<IDataBaseContext>();
            user = new Mock<IUserContext>();

            users = new List<Core.Entities.User>()
            {
                new Core.Entities.User()
                {
                    UID = 1,
                    URID = (int) RoleEnum.User,
                },
                new Core.Entities.User()
                {
                    UID = 2,
                    URID = (int) RoleEnum.Premium,
                },
                new Core.Entities.User()
                {
                    UID = 3,
                    URID = (int) RoleEnum.Support,
                },
                new Core.Entities.User()
                {
                    UID = 4,
                    URID = (int) RoleEnum.Admin,
                },
            };

            context.Setup(x => x.User).Returns(users.AsQueryable());
        }

        [Test]
        public void TestGetIsUserPremiumQueryHandler_UserNotFound_ShouldReturnFalse()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(9);

            var query = new GetIsUserPremiumQuery();
            var handler = new GetIsUserPremiumQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserPremiumQueryHandler_UserIsUser_ShouldReturnFalse()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(1);

            var query = new GetIsUserPremiumQuery();
            var handler = new GetIsUserPremiumQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserPremiumQueryHandler_UserIsPremium_ShouldReturnTrue()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(2);

            var query = new GetIsUserPremiumQuery();
            var handler = new GetIsUserPremiumQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void TestGetIsUserPremiumQueryHandler_UserIsSupport_ShouldReturnFalse()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(3);

            var query = new GetIsUserPremiumQuery();
            var handler = new GetIsUserPremiumQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void TestGetIsUserPremiumQueryHandler_UserIsAdmin_ShouldReturnFalse()
        {
            //Arrange
            user.Setup(x => x.UID).Returns(4);
            
            var query = new GetIsUserPremiumQuery();
            var handler = new GetIsUserPremiumQueryHandler(context.Object, user.Object);

            //Act
            var result = handler.Handle(query);

            //Assert
            ClassicAssert.IsFalse(result);
        }
    }
}