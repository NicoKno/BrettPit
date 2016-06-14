using System;
using System.Collections.Generic;
using System.Linq;
using BrettPit.DataAccess;
using BrettPit.Models;

namespace BrettPit.BusinessLogic
{
    public class UserSetting
    {
        public static UserModel Get(string loginId)
        {
            UserModel resultUser = null;

            using (var db = new DataAccessContext())
            {
                var dbUser = db.users.FirstOrDefault(user => string.Equals(user.loginid, loginId));

                if (dbUser != null)
                {
                    resultUser = new UserModel
                    {
                        Id = dbUser.id,
                        Username = dbUser.name,
                        Password = dbUser.password,
                        Email = dbUser.email,
                        IsAdmin = dbUser.isadmin,
                        LoginGuid = Guid.Parse(dbUser.loginid)
                    };
                }
            }

            return resultUser;
        }

        public static UserModel Get(string name, string password)
        {
            UserModel resultUser = null;

            using (var db = new DataAccessContext())
            {
                var dbUser = db.users.FirstOrDefault(user => user.name == name && user.password == password);

                if (dbUser != null)
                {
                    resultUser = new UserModel
                    {
                        Id = dbUser.id,
                        Username = dbUser.name,
                        Password = dbUser.password,
                        Email = dbUser.email,
                        IsAdmin = dbUser.isadmin,
                        LoginGuid = Guid.Parse(dbUser.loginid)
                    };
                }
            }

            return resultUser;
        }

        public static IEnumerable<UserModel> All()
        {
            using (var db = new DataAccessContext())
            {
                var dbUsers = db.users.Select(user => new UserModel
                {
                    Id = user.id,
                    Username = user.name,
                    Password = user.password,
                    Email = user.email,
                    IsAdmin = user.isadmin,
                    LoginGuid = Guid.Parse(user.loginid)
                });

                return dbUsers;
            }
        }

        public static bool Save(UserModel user)
        {
            bool result;

            try
            {
                using (var db = new DataAccessContext())
                {
                    var newUser = new users
                    {
                        name = user.Username,
                        email = user.Email,
                        isadmin = user.IsAdmin,
                        password = user.Password.CalculateMd5Hash(),
                        loginid = Guid.NewGuid().ToString("N")
                    };
                    db.users.Add(newUser);
                    db.SaveChanges();

                    result = true;
                }
            }
            catch (Exception)
            {
                //TODO: Log exception to log
                result = false;
            }

            return result;
        }
    }
}