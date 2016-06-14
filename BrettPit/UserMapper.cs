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
    }
}