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
                        UserName = dbUser.name,
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
                        UserName = dbUser.name,
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
                    UserName = user.name,
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
                        name = user.UserName,
                        email = user.Email,
                        isadmin = user.IsAdmin,
                        password = user.Password.CalculateMd5Hash(),
                        loginid = user.LoginGuid.ToString("N"),
                        last_login = DateTime.Now
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

        private static string GetRandPW(int Länge)
        {
            string ret = string.Empty;
            System.Text.StringBuilder SB = new System.Text.StringBuilder();
            string Content = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw!öäüÖÄÜß\"§$%&/()=?*#-";
            Random rnd = new Random();
            for (int i = 0; i < Länge; i++)
                SB.Append(Content[rnd.Next(Content.Length)]);
            return SB.ToString();
        }

        public static bool ResetPW(string email)
        {
            bool result = true;

            try
            {
                using (var userDb = new DataAccessContext())
                {
                    if (userDb != null)
                    {
                        var userDS = userDb.users.FirstOrDefault(user => string.Equals(user.email, email));
                        if (userDS != null)
                        {
                            string newPW = GetRandPW(32);
                            //email senden
                            result = EmailUtil.SendPwEmail(email, newPW);
                            if(result)
                            { 
                                //nur wenn die email gesendet werden konnte, neues PW in DB schreiben
                                userDS.password = EncryptionUtil.CalculateMd5Hash(newPW);
                                //notwendig???
                                userDb.Entry(userDS).State = System.Data.Entity.EntityState.Modified;
                                userDb.SaveChanges();
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
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