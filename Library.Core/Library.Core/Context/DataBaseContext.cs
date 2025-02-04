﻿using Microsoft.EntityFrameworkCore;
using Library.Core.Entities;
using Library.Core.Services;

namespace Library.Core.Context
{
    public class DataBaseContext : IDataBaseContext
    {
        private DataContext dataContext;
        private IUserContext user;

        public DataBaseContext(DataContext dataContext, IUserContext user)
        {
            this.dataContext = dataContext;
            this.user = user;
        }

        #region User
        public IQueryable<User> User => dataContext.User;
        public void CreateOrUpdate(User user)
        {
            if (user.UID == default)
                dataContext.User.Add(user);
            else
                dataContext.Entry(user).State = EntityState.Modified;
        }
        public void DeleteUser(User user) => dataContext.User.Remove(user);
        #endregion

        #region Roles
        public IQueryable<Roles> Roles => dataContext.AppRoles;
        #endregion

        #region Users
        public IQueryable<User> AllUsers => dataContext.User;
        #endregion

        #region Books
        public IQueryable<Books> UserBooks => dataContext.Books.Where(x => x.BUID == user.UID);
        public IQueryable<Books> AllBooks => dataContext.Books;
        public void CreateOrUpdate(Books book)
        {
            if (book.BID == default)
                dataContext.Books.Add(book);
            else
                dataContext.Entry(book).State = EntityState.Modified;
        }
        public void DeleteBook(Books book) => dataContext.Books.Remove(book);
        #endregion

        #region Authors
        public IQueryable<Authors> Authors => dataContext.Authors;
        public void CreateOrUpdate(Authors author)
        {
            if (author.AID == default)
                dataContext.Authors.Add(author);
            else
                dataContext.Entry(author).State = EntityState.Modified;
        }
        public void DeleteAuthor(Authors author) => dataContext.Authors.Remove(author);
        #endregion

        #region Publishers
        public IQueryable<Publishers> Publishers => dataContext.Publishers;
        public void CreateOrUpdate(Publishers publisher)
        {
            if (publisher.PID == default)
                dataContext.Publishers.Add(publisher);
            else
                dataContext.Entry(publisher).State = EntityState.Modified;
        }
        public void DeletePublisher(Publishers publisher) => dataContext.Publishers.Remove(publisher);
        #endregion

        #region Reports
        public IQueryable<Reports> Reports => dataContext.Reports;
        public void CreateOrUpdate(Reports report)
        {
            if (report.RID == default)
                dataContext.Reports.Add(report);
            else
                dataContext.Entry(report).State = EntityState.Modified;
        }
        public void DeleteReport(Reports report) => dataContext.Reports.Remove(report);
        #endregion

        public void SaveChanges() => dataContext.SaveChanges();
        public void Dispose() => dataContext.Dispose();
    }
}
