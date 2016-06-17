using System;
using BrettPit.BusinessLogic;
using BrettPit.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace BrettPit
{
    public class UserMapper : IUserMapper
    {
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            var userRecord = UserSetting.Get(identifier.ToString("N"));

            if (userRecord == null)
            {
                return null;
            }

            return new UserIdentity
                       {
                           UserName = userRecord.Username
                       };
        }

        public static Guid? ValidateUser(string username, string password)
        {
            var userRecord = UserSetting.Get(username, password);

            return userRecord.LoginGuid;
        }

        public static Guid? CreateUser(string username, string password, string email)
        {
            var newUser = new UserModel
            {
                Username = username,
                Email = email,
                IsAdmin = false,
                Password = password,
                LoginGuid = Guid.NewGuid()
            };
            var user = UserSetting.Save(newUser);

            return newUser.LoginGuid;
        }
    }
}