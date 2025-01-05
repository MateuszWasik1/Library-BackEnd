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

        #region Tests
        public IQueryable<Tests> Tests => dataContext.Tests.Where(x => x.TUID == user.UID);
        #endregion

        public void SaveChanges() => dataContext.SaveChanges();
        public void Dispose() => dataContext.Dispose();
    }
}
