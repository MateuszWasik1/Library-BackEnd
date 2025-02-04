﻿//using Library.Core.Context;
//using Moq;
//using NUnit.Framework;
//using NUnit.Framework.Legacy;

//namespace Library.UnitTests.CQRS.CommandHandlers.User
//{
//    [TestFixture]
//    public class TestDeleteUserCommandHandler
//    {
//        private Mock<IDataBaseContext>? context;

//        private List<Core.Entities.User>? users;
//        private List<Core.Entities.Categories>? categories;
//        private List<Core.Entities.Tasks>? tasks;
//        private List<Core.Entities.TasksNotes>? tasksNotes;
//        private List<Core.Entities.TasksSubTasks>? tasksSubTasks;
//        private List<Core.Entities.Savings>? savings;
//        private List<Core.Entities.Notes>? notes;

//        [SetUp]
//        public void SetUp()
//        {
//            context = new Mock<IDataBaseContext>();

//            users = new List<Cores.Entities.User> 
//            {
//                new Cores.Entities.User()
//                {
//                    UID = 1,
//                    UGID = new Guid("98dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                },
//                new Cores.Entities.User()
//                {
//                    UID = 2,
//                    UGID = new Guid("99dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                },
//            };

//            categories = new List<Cores.Entities.Categories>()
//            {
//                new Cores.Entities.Categories()
//                {
//                    CID = 1,
//                    CGID = new Guid("00dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    CUID = 1,
//                },
//                new Cores.Entities.Categories()
//                {
//                    CID = 2,
//                    CGID = new Guid("01dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    CUID = 1,
//                },
//                new Cores.Entities.Categories()
//                {
//                    CID = 2,
//                    CGID = new Guid("02dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    CUID = 66,
//                },
//            };

//            tasks = new List<Cores.Entities.Tasks>()
//            {
//                new Cores.Entities.Tasks()
//                {
//                    TID = 1,
//                    TGID = new Guid("03dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TUID = 1,
//                },
//                new Cores.Entities.Tasks()
//                {
//                    TID = 2,
//                    TGID = new Guid("04dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TUID = 1,
//                },
//                new Cores.Entities.Tasks()
//                {
//                    TID = 3,
//                    TGID = new Guid("05dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TUID = 66,
//                },
//            };

//            tasksNotes = new List<Cores.Entities.TasksNotes>()
//            {
//                new Cores.Entities.TasksNotes()
//                {
//                    TNID = 1,
//                    TNGID = new Guid("06dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TNUID = 1,
//                },
//                new Cores.Entities.TasksNotes()
//                {
//                    TNID = 2,
//                    TNGID = new Guid("07dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TNUID = 1,
//                },
//                new Cores.Entities.TasksNotes()
//                {
//                    TNID = 3,
//                    TNGID = new Guid("08dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TNUID = 66,
//                },
//            };

//            tasksSubTasks = new List<Cores.Entities.TasksSubTasks>()
//            {
//                new Cores.Entities.TasksSubTasks()
//                {
//                    TSTID = 1,
//                    TSTGID = new Guid("12dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TSTUID = 1,
//                },
//                new Cores.Entities.TasksSubTasks()
//                {
//                    TSTID = 2,
//                    TSTGID = new Guid("13dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TSTUID = 1,
//                },
//                new Cores.Entities.TasksSubTasks()
//                {
//                    TSTID = 3,
//                    TSTGID = new Guid("14dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    TSTUID = 66,
//                },
//            };

//            savings = new List<Cores.Entities.Savings>()
//            {
//                new Cores.Entities.Savings()
//                {
//                    SID = 1,
//                    SGID = new Guid("09dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    SUID = 1,
//                },
//                new Cores.Entities.Savings()
//                {
//                    SID = 2,
//                    SGID = new Guid("10dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    SUID = 1,
//                },
//                new Cores.Entities.Savings()
//                {
//                    SID = 3,
//                    SGID = new Guid("11dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    SUID = 66,
//                },
//            };

//            notes = new List<Cores.Entities.Notes>()
//            {
//                new Cores.Entities.Notes()
//                {
//                    NID = 1,
//                    NGID = new Guid("12dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    NUID = 1,
//                },
//                new Cores.Entities.Notes()
//                {
//                    NID = 2,
//                    NGID = new Guid("13dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    NUID = 1,
//                },
//                new Cores.Entities.Notes()
//                {
//                    NID = 3,
//                    NGID = new Guid("14dacc1d-7bee-4635-9c4c-9404a4af80dd"),
//                    NUID = 66,
//                },
//            };

//            context.Setup(x => x.AllUsers).Returns(users.AsQueryable());
//            context.Setup(x => x.AllCategories).Returns(categories.AsQueryable());
//            context.Setup(x => x.AllTasks).Returns(tasks.AsQueryable());
//            context.Setup(x => x.AllTasksNotes).Returns(tasksNotes.AsQueryable());
//            context.Setup(x => x.AllTasksSubTasks).Returns(tasksSubTasks.AsQueryable());
//            context.Setup(x => x.AllSavings).Returns(savings.AsQueryable());
//            context.Setup(x => x.AllNotes).Returns(notes.AsQueryable());

//            context.Setup(x => x.DeleteUser(It.IsAny<Cores.Entities.User>())).Callback<Cores.Entities.User>(user => users.Remove(user));
//            context.Setup(x => x.DeleteCategory(It.IsAny<Cores.Entities.Categories>())).Callback<Cores.Entities.Categories>(category => categories.Remove(category));
//            context.Setup(x => x.DeleteTask(It.IsAny<Cores.Entities.Tasks>())).Callback<Cores.Entities.Tasks>(task => tasks.Remove(task));
//            context.Setup(x => x.DeleteTaskNotes(It.IsAny<Cores.Entities.TasksNotes>())).Callback<Cores.Entities.TasksNotes>(taskNote => tasksNotes.Remove(taskNote));
//            context.Setup(x => x.DeleteTaskSubTask(It.IsAny<Cores.Entities.TasksSubTasks>())).Callback<Cores.Entities.TasksSubTasks>(tasksSubTask => tasksSubTasks.Remove(tasksSubTask));
//            context.Setup(x => x.DeleteSaving(It.IsAny<Cores.Entities.Savings>())).Callback<Cores.Entities.Savings>(saving => savings.Remove(saving));
//            context.Setup(x => x.DeleteNote(It.IsAny<Cores.Entities.Notes>())).Callback<Cores.Entities.Notes>(note => notes.Remove(note));
//        }

//        [Test]
//        public void TestDeleteUserCommandHandler_UserNotFound_ShouldThrowUserNotFoundExceptions()
//        {
//            //Arrange
//            var query = new DeleteUserCommand() { UGID = new Guid("69dacc1d-7bee-4635-9c4c-9404a4af80dd") };
//            var handler = new DeleteUserCommandHandler(context.Object);

//            //Act
//            //Assert
//            Assert.Throws<UserNotFoundExceptions>(() => handler.Handle(query));
//        }

//        [Test]
//        public void TestDeleteUserCommandHandler_UserFound_ShouldDeleteUserAndAllHisData()
//        {
//            //Arrange
//            var query = new DeleteUserCommand() { UGID = users[0].UGID };
//            var handler = new DeleteUserCommandHandler(context.Object);

//            //Act
//            handler.Handle(query);

//            //Assert
//            ClassicAssert.AreEqual(1, users.Count);
//            ClassicAssert.AreEqual(1, categories.Count);
//            ClassicAssert.AreEqual(1, tasks.Count);
//            ClassicAssert.AreEqual(1, tasksNotes.Count);
//            ClassicAssert.AreEqual(1, tasksSubTasks.Count);
//            ClassicAssert.AreEqual(1, savings.Count);
//            ClassicAssert.AreEqual(1, notes.Count);

//            context.Verify(x => x.DeleteUser(It.IsAny<Cores.Entities.User>()), Times.Once);
//            context.Verify(x => x.DeleteCategory(It.IsAny<Cores.Entities.Categories>()), Times.Exactly(2));
//            context.Verify(x => x.DeleteTask(It.IsAny<Cores.Entities.Tasks>()), Times.Exactly(2));
//            context.Verify(x => x.DeleteTaskNotes(It.IsAny<Cores.Entities.TasksNotes>()), Times.Exactly(2));
//            context.Verify(x => x.DeleteTaskSubTask(It.IsAny<Cores.Entities.TasksSubTasks>()), Times.Exactly(2));
//            context.Verify(x => x.DeleteSaving(It.IsAny<Cores.Entities.Savings>()), Times.Exactly(2));
//            context.Verify(x => x.DeleteNote(It.IsAny<Cores.Entities.Notes>()), Times.Exactly(2));
//        }
//    }
//}
