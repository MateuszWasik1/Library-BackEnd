using Library.Core.Controllers;
using Library.Core.CQRS.Dispatcher;
using Library.Core.CQRS.Resources.Roles.Queries;
using Library.Core.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace Library.UnitTests.Controllers
{
    [TestFixture]
    public class TestRolesController
    {
        private Mock<IDispatcher> dispatcher;

        [SetUp]
        public void SetUp() => dispatcher = new Mock<IDispatcher>();

        [Test]
        public void TestRolesController_GetUserRoles_ShouldDispatch_GetUserRolesQuery()
        {
            //Arrange
            var controller = new RolesController(dispatcher.Object);

            //Act
            controller.GetUserRoles();

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetUserRolesQuery, RolesViewModel>(It.IsAny<GetUserRolesQuery>()), Times.Once);
        }

        [Test]
        public void TestRolesController_GetIsUserSupport_ShouldDispatch_GetIsUserPremiumQuery()
        {
            //Arrange
            var controller = new RolesController(dispatcher.Object);

            //Act
            controller.GetIsUserPremium();

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetIsUserPremiumQuery, bool>(It.IsAny<GetIsUserPremiumQuery>()), Times.Once);
        }

        [Test]
        public void TestRolesController_GetIsUserSupport_ShouldDispatch_GetIsUserSupportQuery()
        {
            //Arrange
            var controller = new RolesController(dispatcher.Object);

            //Act
            controller.GetIsUserSupport();

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetIsUserSupportQuery, bool>(It.IsAny<GetIsUserSupportQuery>()), Times.Once);
        }

        [Test]
        public void TestRolesController_GetIsUserAdmin_ShouldDispatch_GetIsUserAdminQuery()
        {
            //Arrange
            var controller = new RolesController(dispatcher.Object);

            //Act
            controller.GetIsUserAdmin();

            //Assert
            dispatcher.Verify(x => x.DispatchQuery<GetIsUserAdminQuery, bool>(It.IsAny<GetIsUserAdminQuery>()), Times.Once);
        }
    }
}