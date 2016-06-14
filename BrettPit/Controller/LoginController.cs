using System;
using System.Dynamic;
using BrettPit.BusinessLogic;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;

namespace BrettPit.Controller
{
    public class LoginController : NancyModule
    {
        public LoginController()
        {
            Get["/login"] = GetLoginView;
            Post["/login"] = Login;
            Get["/logout"] = LogOut;
        }

        private dynamic GetLoginView(dynamic parameters)
        {
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;

            return View["login", model];
        }

        private dynamic Login(dynamic parameters)
        {
            var username = (string)Request.Form.Username;
            var password = (string)Request.Form.Password;

            var userGuid = UserMapper.ValidateUser(username, password.CalculateMd5Hash());
            
            if (userGuid == null)
            {
                return Context.GetRedirect("~/login?error=true&username=" + (string)Request.Form.Username);
            }

            DateTime? expiry = null;
            if (Request.Form.RememberMe.HasValue)
            {
                expiry = DateTime.Now.AddDays(7);
            }

            return this.LoginAndRedirect(userGuid.Value, expiry);
        }

        private dynamic LogOut(dynamic arg)
        {
            return this.LogoutAndRedirect("~/");
        }


    }
}