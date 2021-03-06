﻿using System;
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
                var dbUser = db.users.FirstOrDefault(user => (user.name == name || user.email == name) && user.password == password);

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
            IEnumerable<UserModel> dbUsers;
            using (var db = new DataAccessContext())
            {
                dbUsers = db.users.ToList().Select(user => new UserModel
                {
                    Id = user.id,
                    UserName = user.name,
                    Password = user.password,
                    Email = user.email,
                    IsAdmin = user.isadmin,
                    LoginGuid = Guid.Parse(user.loginid)
                });
            }

            return dbUsers.ToList();
        }

        public static List<UserModel> GetAll()
        {
            IEnumerable<UserModel> dbUsers;
            using (var db = new DataAccessContext())
            {
                dbUsers = db.users.ToList().Select(user => new UserModel
                {
                    Id = user.id,
                    UserName = user.name,
                    Password = user.password,
                    Email = user.email,
                    IsAdmin = user.isadmin,
                    LoginGuid = Guid.Parse(user.loginid)
                });
            }

            return dbUsers.ToList();
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

        private static string GetRandomPassword(int Länge)
        {
            var stringBuilder = new System.Text.StringBuilder();
            const string content = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw!öäüÖÄÜß\"§$%&/()=?*#-";
            var rnd = new Random();
            for (var i = 0; i < Länge; i++)
            {
                stringBuilder.Append(content[rnd.Next(content.Length)]);
            }
            return stringBuilder.ToString();
        }

        public static bool ResetPassword(string email)
        {
            bool result;

            try
            {
                using (var userDb = new DataAccessContext())
                {
                    var userRecord = userDb.users.FirstOrDefault(user => string.Equals(user.email, email));
                    if (userRecord != null)
                    {
                        var newPassword = GetRandomPassword(32);
                        //email senden
                        result = EmailUtil.SendPasswordEmail(email, newPassword);
                        if (result)
                        {
                            //nur wenn die email gesendet werden konnte, neues PW in DB schreiben
                            userRecord.password = newPassword.CalculateMd5Hash();
                            //notwendig???
                            userDb.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                            userDb.SaveChanges();
                        }
                    }
                    else
                    {
                        result = false;
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

        public static bool ResetPassword(int uid)
        {
            bool result;

            try
            {
                using (var userDb = new DataAccessContext())
                {
                    var userRecord = userDb.users.Find(uid);
                    if (userRecord != null)
                    {
                        var newPassword = GetRandomPassword(32);
                        //email senden
                        result = EmailUtil.SendPasswordEmail(userRecord.email, newPassword);
                        if (result)
                        {
                            //nur wenn die email gesendet werden konnte, neues PW in DB schreiben
                            userRecord.password = newPassword.CalculateMd5Hash();
                            //notwendig???
                            userDb.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                            userDb.SaveChanges();
                        }
                    }
                    else
                    {
                        result = false;
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

        public static bool ChangeNameAndEmail(int userid, string NewUsername, string NewUseremail)
        {
            bool result;

            try
            {
                using (var userDb = new DataAccessContext())
                {
                    var userRecord = userDb.users.Find(userid);
                    if (userRecord != null)
                    {
                        if (userRecord.email != NewUseremail)
                            CreateEmailChangeLog(userRecord.email, NewUseremail);
                        userRecord.name = NewUsername;
                        userRecord.email = NewUseremail;
                        userDb.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                        userDb.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        result = false;
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

        private static void CreateEmailChangeLog(string email, string newUseremail)
        {
            //email change logging here
        }

        public static bool ChangePassword(int userid, string NewPassword)
        {
            bool result;

            try
            {
                using (var userDb = new DataAccessContext())
                {
                    var userRecord = userDb.users.Find(userid);
                    if (userRecord != null)
                    {
                        userRecord.password = NewPassword.CalculateMd5Hash();
                        userDb.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                        userDb.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        result = false;
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

        public static bool ChangeAdminState(int userid, int adminid)
        {
            bool result;
            string oldstate = "";
            string newstate = "";

            try
            {
                using (var userDb = new DataAccessContext())
                {
                    var userRecord = userDb.users.Find(userid);
                    if (userRecord != null)
                    {
                        oldstate = userRecord.isadmin.ToString();
                        userRecord.isadmin = !userRecord.isadmin;
                        userDb.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                        userDb.SaveChanges();
                        newstate = userRecord.isadmin.ToString();
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch (Exception)
            {
                //TODO: Log exception to log
                result = false;
            }
            finally
            {
                using (var LogDb = new DataAccessContext())
                {
                    var logentry = new log_admin();
                    logentry.timestamp = DateTime.Now;
                    logentry.uid_affected = userid;
                    logentry.uid_performed = adminid;
                    logentry.changed_from = oldstate;
                    logentry.changed_to = newstate;

                    LogDb.log_admin.Add(logentry);
                    LogDb.SaveChanges();
                }
            }

            return result;
        }

        public static bool DeleteAccount(int userid)
        {
            bool result;

            try
            {
                //deleting the corresponding datasets in table "pairings"
                using (var matchDb = new DataAccessContext())
                {
                    matchDb.pairings.RemoveRange(matchDb.pairings.Where(match => match.uid1 == userid || match.uid2 == userid));
                    matchDb.SaveChanges();
                }

                //deleting the corresponding datasets in table "elos"
                using (var eloDb = new DataAccessContext())
                {
                    eloDb.eloes.RemoveRange(eloDb.eloes.Where(elo => elo.uid == userid));
                    eloDb.SaveChanges();
                }

                //deleting the corresponding datasets in table "users"
                using (var userDb = new DataAccessContext())
                {
                    userDb.users.Remove(userDb.users.Find(userid));
                    userDb.SaveChanges();
                }
                result = true;
            }
            catch (Exception)
            {
                //TODO: Log exception to log
                result = false;
            }

            return result;
        }

        public static bool UsernameExists(string username)
        {
            bool result;

            using (var db = new DataAccessContext())
            {
                result = db.users.Any(dbUser => dbUser.name == username);
            }

            return result;
        }

        public static bool EmailExists(string email)
        {
            bool result;

            using (var db = new DataAccessContext())
            {
                result = db.users.Any(dbUser => dbUser.email == email);
            }

            return result;
        }
    }
}