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
            Post["/register"] = RegisterUser;
        }

        private object RegisterUser(object arg)
        {
            var username = (string) Request.Form.Username;
            var password = (string) Request.Form.Password;
            var repeat = (string) Request.Form.Repeat;
            var email = (string) Request.Form.Email;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || password != repeat)
            {
                return Context.GetRedirect("~/login?repeatError=true");
            }

            var userGuid = UserMapper.CreateUser(username, password, email);

            if (userGuid == null)
            {
                return Context.GetRedirect("~/login?error=true&username=" + (string)Request.Form.Username);
            }

            return this.LoginAndRedirect(userGuid.Value, fallbackRedirectUrl:"~/games");
        }

        private dynamic GetLoginView(dynamic parameters)
        {
            dynamic model = new ExpandoObject();
            model.Errored = Request.Query.error.HasValue;
            model.RegisterErrored = Request.Query.repeatError.HasValue;
            model.Email = "geht@imail.com";
            return View["login", model];
        }

        private dynamic Login(dynamic parameters)
        {
            var username = (string)Request.Form.Username;
            var password = (string)Request.Form.Password;

            // check if user exists and password matches
            var userGuid = UserMapper.ValidateUser(username, password.CalculateMd5Hash());
            
            if (userGuid == null)
            {
                return Context.GetRedirect("~/login?error=true&username=" + (string)Request.Form.Username);
            }

            // check if user wants to be remembered
            DateTime? expiry = null;
            if (Request.Form.RememberMe.HasValue)
            {
                expiry = DateTime.Now.AddDays(7);
            }

            return this.LoginAndRedirect(userGuid.Value, expiry);
        }

        private dynamic LogOut(dynamic arg)
        {
            return this.LogoutAndRedirect("~/games");
        }


    }
}
