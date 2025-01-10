using Library.Core.Entities;

namespace Library.Core.Context
{
    public interface IDataBaseContext : IDisposable
    {
        #region User
        IQueryable<User> User { get; }
        void CreateOrUpdate(User user);
        void DeleteUser(User user);
        #endregion

        #region Roles
        IQueryable<Roles> Roles { get; }
        #endregion

        #region Users
        IQueryable<User> AllUsers { get; }
        #endregion

        #region Books
        IQueryable<Books> UserBooks { get; }
        IQueryable<Books> AllBooks { get; }
        void CreateOrUpdate(Books books);
        void DeleteBook(Books books);
        #endregion

        #region Authors
        IQueryable<Authors> Authors { get; }
        void CreateOrUpdate(Authors authors);
        void DeleteAuthor(Authors authors);
        #endregion

        #region Publishers
        IQueryable<Publishers> Publishers { get; }
        void CreateOrUpdate(Publishers publisher);
        void DeletePublisher(Publishers publisher);
        #endregion

        void SaveChanges();
    }
}
